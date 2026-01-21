using BugReportApp.Controllers;
using BugReportApp.ModelDB;
using BugReportApp.Model.Main;

namespace BugReportApp.Services.Main
{
    public interface IMainService
    {
        ProjectData? GetFullInfoAboutProject(int projectId);
        List<TestersTask> GetTasks(int projectId, int testerId);
        string? AddMainTesterToProject(string email, int projectId);
        string? AddDeveloperToProject(string email, int projectId);
        string? AddTesterToProject(string email, int projectId);
        List<(BugReport Bug, string Status)> GetAllBugReportsForProject(int projectId, int testerId, int developerId);
    }
}
