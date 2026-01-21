namespace BugReportApp.Model.Common
{
    public class ProjectStatisticsDeveloper
    {
        public int AssignedBugs { get; set; }
        public int SuccessfullyFixedBugs { get; set; }
        public int AverageFixTime { get; set; }
        public int ReturnedForReworkPercent { get; set; }
        public BugsByPriority BugsByPriority { get; set; }
        public BugsBySeverity BugsBySeverity { get; set; }

    }
}
