using BugReportApp.Model.Project;

namespace BugReportApp.Services.Project
{
    public interface IProjectService
    {
        Dictionary<string, List<object>> GetProjectsByUser(int userId);
    }
}
