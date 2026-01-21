using System.ComponentModel.DataAnnotations;

namespace BugReportApp.ModelDB
{
    public class BugReportUpdateHistory
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "BugId is required")]
        public int BugId { get; set; }

        [Required(ErrorMessage = "ChangedBy is required")]
        public int ChangedBy { get; set; }

        [Required(ErrorMessage = "NewValue is required")]
        public string NewValue { get; set; } = string.Empty;

        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime ChangedAt { get; set; }

       

    }
}
