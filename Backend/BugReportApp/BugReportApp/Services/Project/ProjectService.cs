using BugReportApp.Contexts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BugReportApp.Model.Project;

namespace BugReportApp.Services.Project
{
    public class ProjectService : IProjectService
    {
        private readonly DBContext _dbContext;

        public ProjectService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Dictionary<string, List<object>> GetProjectsByUser(int userId)
        {
            var userProjects = _dbContext.userProject
                .Where(up => up.UserId == userId)
                .Include(up => up.Project) // чтобы загружать данные проекта
                .Select(up => new
                {
                    ProjectId = up.Project.ID,
                    ProjectTitle = up.Project.Title,
                    Role = up.Role
                })
                .ToList();

            var headDeveloper = new List<object>();
            var headTester = new List<object>();
            var developer = new List<object>();
            var tester = new List<object>();

            foreach (var up in userProjects)
            {
                var projectInfo = new { id = up.ProjectId, title = up.ProjectTitle };

                switch (up.Role)
                {
                    case "HEAD_DEVELOPER":
                        headDeveloper.Add(projectInfo);
                        break;
                    case "HEAD_TESTER":
                        headTester.Add(projectInfo);
                        break;
                    case "DEVELOPER":
                        developer.Add(projectInfo);
                        break;
                    case "TESTER":
                        tester.Add(projectInfo);
                        break;
                }
            }

            return new Dictionary<string, List<object>>
            {
                { "headDeveloper", headDeveloper },
                { "headTester", headTester },
                { "developer", developer },
                { "tester", tester }
            };
        }
    }
}
