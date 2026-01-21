namespace BugReportApp.Model.Main
{
    public class ProjectData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UserShortData> Developers { get; set; }
        public List<UserShortData> Testers { get; set; }
        public UserShortData HeadDeveloper { get; set; }
        public UserShortData HeadTester { get; set; }
    }
}
