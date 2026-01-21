namespace BugReportApp.Model.BugReportFlow
{
    public class BugReportDeveloperAssignmentRequest
    {
        public int BugId { get; set; }
        public int HeadDeveloperId { get; set; }
        public int DeveloperId { get; set; }
    }
}
