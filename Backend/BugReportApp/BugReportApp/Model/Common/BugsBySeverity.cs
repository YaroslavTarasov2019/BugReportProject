namespace BugReportApp.Model.Common
{
    public class BugsBySeverity
    {
        public int Blocker { get; set; }
        public int Critical { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Trivial { get; set; }
    }
}
