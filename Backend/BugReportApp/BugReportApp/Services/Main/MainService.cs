using BugReportApp.Controllers;
using BugReportApp.ModelDB;
using BugReportApp.Contexts;
using Microsoft.EntityFrameworkCore;
using BugReportApp.Model.Main;

namespace BugReportApp.Services.Main
{
    public class MainService : IMainService
    {
        private readonly DBContext _dbContext;

        public MainService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProjectData? GetFullInfoAboutProject(int projectId)
        {
            var projectWithUsers = _dbContext.project
                .Where(p => p.ID == projectId)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .FirstOrDefault();

            if (projectWithUsers == null)
                return null;

            var project = new ProjectData
            {
                Id = projectWithUsers.ID,
                Title = projectWithUsers.Title,
                Description = projectWithUsers.Description,
                Developers = new List<UserShortData>(),
                Testers = new List<UserShortData>(),
                HeadDeveloper = new UserShortData(),
                HeadTester = new UserShortData()
            };

            foreach (var o in projectWithUsers.UserProjects)
            {
                var userData = new UserShortData
                {
                    ID = o.User.ID,
                    Name = o.User.Name,
                    Patronymic = o.User.Patronymic,
                    Surname = o.User.Surname,
                    PhoneNumber = o.User.PhoneNumber,
                    Email = o.User.Email,
                    Role = o.Role
                };

                switch (o.Role)
                {
                    case "TESTER":
                        project.Testers.Add(userData);
                        break;
                    case "DEVELOPER":
                        project.Developers.Add(userData);
                        break;
                    case "HEAD_TESTER":
                        project.HeadTester = userData;
                        break;
                    case "HEAD_DEVELOPER":
                        project.HeadDeveloper = userData;
                        break;
                }
            }

            return project;
        }

        public List<TestersTask> GetTasks(int projectId, int testerId)
        {
            bool exists = _dbContext.userProject
                .Any(up => up.ProjectId == projectId && up.UserId == testerId);

            if (!exists)
                return new List<TestersTask>();

            var tasks = _dbContext.testersTask
                .Where(p => p.ProjectID == projectId && p.TesterID == testerId)
                .ToList();

            return tasks;
        }

        private string? AddWorkerToProject(string email, int projectId, string role)
        {
            var user = _dbContext.userData.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return "Email not found";

            var existUserProject = _dbContext.userProject
                .FirstOrDefault(u => u.UserId == user.ID && u.ProjectId == projectId);

            if (existUserProject != null)
                return "User already in project";

            var userProject = new UserProject
            {
                UserId = user.ID,
                ProjectId = projectId,
                Role = role
            };

            _dbContext.userProject.Add(userProject);
            _dbContext.SaveChanges();

            return null;
        }

        public string? AddMainTesterToProject(string email, int projectId)
            => AddWorkerToProject(email, projectId, "HEAD_TESTER");

        public string? AddDeveloperToProject(string email, int projectId)
            => AddWorkerToProject(email, projectId, "DEVELOPER");

        public string? AddTesterToProject(string email, int projectId)
            => AddWorkerToProject(email, projectId, "TESTER");

        public List<(BugReport Bug, string Status)> GetAllBugReportsForProject(int projectId, int testerId, int developerId)
        {
            List<BugReport> result;

            if (testerId != -1)
            {
                result = _dbContext.bugReport
                    .Where(u => u.ProjectId == projectId && u.TesterId == testerId)
                    .ToList();
            }
            else if (developerId != -1)
            {
                result = _dbContext.bugReport
                    .Where(u => u.ProjectId == projectId && u.DeveloperId == developerId)
                    .ToList();
            }
            else
            {
                result = _dbContext.bugReport
                    .Where(u => u.ProjectId == projectId)
                    .ToList();
            }

            if (!result.Any())
                return new List<(BugReport, string)>();

            var bugIds = result.Select(b => b.ID).ToList();

            var latestStatuses = _dbContext.bugReportUpdateHistory
                .Where(h => bugIds.Contains(h.BugId))
                .GroupBy(h => h.BugId)
                .Select(g => new
                {
                    BugId = g.Key,
                    LastStatus = g.OrderByDescending(x => x.ID).FirstOrDefault().NewValue
                })
                .ToDictionary(x => x.BugId, x => x.LastStatus);

            var bugsWithStatus = result.Select(b => (
                Bug: b,
                Status: latestStatuses.ContainsKey(b.ID) ? latestStatuses[b.ID] : "Unknown"
            )).ToList();

            return bugsWithStatus;
        }
    }
}
