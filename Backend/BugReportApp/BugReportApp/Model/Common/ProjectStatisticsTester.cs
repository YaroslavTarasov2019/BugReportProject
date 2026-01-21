namespace BugReportApp.Model.Common
{
    public class ProjectStatisticsTester
    {
        public int CreatedBugs { get; set; }
        public BugsByPriority BugsByPriority { get; set; }
        public BugsBySeverity BugsBySeverity { get; set; }

    }
}
