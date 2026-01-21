namespace BugReportApp.Services.BugReportFlow
{
    public interface IBugReportFlowService
    {
        Task<bool> PassBugReportOnAsync(int bugId, int userId);
        Task<bool> RejectBugReportAsync(int bugId, int userId);
        Task<bool> ConfirmBugFixAsync(int bugId, int testerId, int headTesterId);
        Task<bool> NotConfirmBugFixAsync(int bugId, int userId);
        Task<bool> AssignDeveloperAsync(int bugId, int developerId, int headDeveloperId);
    }

}
