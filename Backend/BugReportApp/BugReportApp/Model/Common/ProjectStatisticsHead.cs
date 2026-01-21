namespace BugReportApp.Model.Common
{
    public class ProjectStatisticsHead
    {
        public int FoundBugs { get; set; }
        public int ClosedBugs { get; set; }
        public int VerifiedBugs { get; set; }
        public BugsByPriority BugsByPriority { get; set; }
        public BugsBySeverity BugsBySeverity { get; set; }

    }
}
