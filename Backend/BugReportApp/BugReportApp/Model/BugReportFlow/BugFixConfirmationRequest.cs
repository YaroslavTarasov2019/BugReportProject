namespace BugReportApp.Model.BugReportFlow
{
    public class BugFixConfirmationRequest
    {
        public int BugId { get; set; }
        public int TesterId { get; set; } = -1;
        public int HeadTesterId { get; set; } = -1;
    }
}
