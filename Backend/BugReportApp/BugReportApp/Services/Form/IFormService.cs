using BugReportApp.Controllers;
using BugReportApp.ModelDB;
using BugReportApp.Model.Form;

namespace BugReportApp.Services.Form
{
    public interface IFormService
    {
        Task<bool> ProjectExistsAsync(string title, int userId);
        Task CreateProjectAsync(ProjectCreateDto model);
        Task<bool> ProjectExistsByIdAsync(int projectId);
        Task<bool> UserExistsAsync(int userId);
        Task CreateBugReportAsync(BugReportCreateDto model);
        Task<UserData?> GetUserByIdAsync(int userId);
        Task<BugReportApp.ModelDB.Project?> GetProjectByIdAsync(int projectId);
    }
}
