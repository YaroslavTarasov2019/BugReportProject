using System.ComponentModel.DataAnnotations;

namespace BugReportApp.ModelDB
{
    public class UserData
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Patronymic is required")]
        public string Patronymic { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "PasswordHash is required")]
        public string PasswordHash { get; set; } = string.Empty;



        public ICollection<UserProject> UserProjects { get; set; }

    }
}
