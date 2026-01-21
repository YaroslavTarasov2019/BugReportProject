using System.ComponentModel.DataAnnotations;

namespace BugReportApp.ModelDB
{
    public class TestersTask
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Patronymic is required")]
        public int TesterID { get; set; }

        [Required(ErrorMessage = "Patronymic is required")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Patronymic is required")]
        public string TaskText { get; set; } = string.Empty;

        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime CreatedAt { get; set; }
    }
}
