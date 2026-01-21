using BugReportApp.Controllers;
using BugReportApp.ModelDB;
using BugReportApp.Contexts;
using Microsoft.EntityFrameworkCore;
using BugReportApp.Model.Form;

namespace BugReportApp.Services.Form
{
    public class FormService : IFormService
    {
        private readonly DBContext _dbContext;

        public FormService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ProjectExistsAsync(string title, int userId)
        {
            return await _dbContext.project
                .Where(p => p.Title.Contains(title) && p.UserProjects.Any(up => up.UserId == userId))
                .AnyAsync();
        }

        public async Task CreateProjectAsync(ProjectCreateDto model)
        {
            var newProject = new BugReportApp.ModelDB.Project
            {
                Title = model.Title,
                Description = model.Description,
                DateNow = DateTime.Now
            };

            await _dbContext.project.AddAsync(newProject);
            await _dbContext.SaveChangesAsync();

            var userProject = new UserProject
            {
                UserId = model.UserId,
                ProjectId = newProject.ID,
                Role = "HEAD_DEVELOPER"
            };

            await _dbContext.userProject.AddAsync(userProject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ProjectExistsByIdAsync(int projectId)
        {
            return await _dbContext.project.AnyAsync(p => p.ID == projectId);
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _dbContext.userData.AnyAsync(u => u.ID == userId);
        }

        public async Task CreateBugReportAsync(BugReportCreateDto model)
        {
            var newBugReport = new BugReport
            {
                Title = model.Title,
                Description = model.Description,
                StepsToReproduce = model.Steps,
                Severity = model.Severity,
                Priority = model.Priority,
                CreatedAt = model.Date,
                ProjectId = model.ProjectID,
                TesterId = model.UserID,
                FileId = model.FileId
            };

            await _dbContext.bugReport.AddAsync(newBugReport);
            await _dbContext.SaveChangesAsync();

            var bugReportUpdateHistory = new BugReportUpdateHistory
            {
                BugId = newBugReport.ID,
                ChangedBy = model.UserID,
                NewValue = "New",
                ChangedAt = model.Date
            };

            await _dbContext.bugReportUpdateHistory.AddAsync(bugReportUpdateHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserData?> GetUserByIdAsync(int userId)
        {
            return await _dbContext.userData.FirstOrDefaultAsync(u => u.ID == userId);
        }

        public async Task<BugReportApp.ModelDB.Project?> GetProjectByIdAsync(int projectId)
        {
            return await _dbContext.project.FirstOrDefaultAsync(p => p.ID == projectId);
        }
    }
}
