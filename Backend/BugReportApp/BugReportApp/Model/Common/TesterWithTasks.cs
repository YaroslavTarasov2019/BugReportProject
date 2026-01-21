namespace BugReportApp.Model.Common
{
    public class TesterWithTasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<string> Tasks { get; set; }
    }
}
