using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;

namespace BugReportApp.ModelDB
{
    public class FileData
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "RelativePath is required")]
        public string RelativePath { get; set; } = string.Empty;

        [Required(ErrorMessage = "MimeType is required")]
        public string MimeType { get; set; } = string.Empty;

        [Required(ErrorMessage = "FileSize is required")]
        public int FileSize { get; set; }

        [Required(ErrorMessage = "UploadDate is required")]
        public DateTime UploadDate { get; set; }
    }
}
