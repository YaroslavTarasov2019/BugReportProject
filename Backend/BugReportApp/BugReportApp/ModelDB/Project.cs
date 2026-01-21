using System.ComponentModel.DataAnnotations;

namespace BugReportApp.ModelDB
{
    public class Project
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "DateNow is required")]
        public DateTime DateNow { get; set; }


        public ICollection<UserProject> UserProjects { get; set; }
    }
}
