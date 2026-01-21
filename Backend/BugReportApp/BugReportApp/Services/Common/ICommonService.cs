using BugReportApp.Controllers;
using BugReportApp.ModelDB;
using BugReportApp.Model.Common;

namespace BugReportApp.Services.Common
{
    public interface ICommonService
    {
        ProjectStatisticsHead GetStatisticsInformationHead(RoleAndProjectRequest model);
        ProjectStatisticsDeveloper GetStatisticsInformationDeveloper(RoleAndProjectRequest model);
        ProjectStatisticsTester GetStatisticsInformationTester(RoleAndProjectRequest model);
        List<TesterWithTasks> GetTestersByProject(int projectId);
        (BugReport, List<BugReportUpdateHistory>) GetBugById(int bugId);
        List<object> GetDevelopersByProjectId(int projectId);
        void AssignTask(TaskAssignment model);
    }
}
