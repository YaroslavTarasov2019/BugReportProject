namespace BugReportApp.Model.Form
{
    public class BugReportCreateDto
    {
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }
        public string Priority { get; set; }
        public string Severity { get; set; }
        public DateTime Date { get; set; }
        public int? FileId { get; set; }
    }
}
