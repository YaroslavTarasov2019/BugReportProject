namespace BugReportApp.Model.Main
{
    public class UserShortData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
