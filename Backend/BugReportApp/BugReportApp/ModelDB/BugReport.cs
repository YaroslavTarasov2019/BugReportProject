using System.ComponentModel.DataAnnotations;

namespace BugReportApp.ModelDB
{
    public class BugReport
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "StepsToReproduce is required")]
        public string StepsToReproduce { get; set; } = string.Empty;

        [Required(ErrorMessage = "Severity is required")]
        public string Severity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority is required")]
        public string Priority { get; set; } = string.Empty;

        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "ProjectID is required")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "TesterID is required")]
        public int TesterId { get; set; }


        public int? DeveloperId { get; set; }
        public int? FileId { get; set; }

    }
}
