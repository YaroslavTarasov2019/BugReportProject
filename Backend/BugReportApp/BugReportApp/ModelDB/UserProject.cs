using BugReportApp.Controllers;
using System.ComponentModel.DataAnnotations;

namespace BugReportApp.ModelDB
{
    public class UserProject
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        public UserData User { get; set; }


        [Required(ErrorMessage = "ProjectId is required")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }



        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = string.Empty;
    }
}
