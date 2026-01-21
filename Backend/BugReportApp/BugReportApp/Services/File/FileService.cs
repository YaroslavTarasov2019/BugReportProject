using BugReportApp.ModelDB;
using BugReportApp.Contexts;

namespace BugReportApp.Services.File
{
    public class FileService : IFileService
    {
        private readonly string _uploadPath;
        private readonly DBContext _dbContext;

        public FileService(DBContext dbContext)
        {
            _dbContext = dbContext;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        public async Task<(string fileName, int fileId)> UploadAsync(IFormFile file, int userId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            var ext = Path.GetExtension(file.FileName).ToLower();
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".gif" };
            if (!allowedExtensions.Contains(ext))
                throw new ArgumentException("Invalid file type.");

            var userFolder = Path.Combine(_uploadPath, userId.ToString());
            if (!Directory.Exists(userFolder))
                Directory.CreateDirectory(userFolder);

            var filePath = Path.Combine(userFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileData = new FileData
            {
                Name = file.FileName,
                RelativePath = Path.Combine(userId.ToString(), file.FileName).Replace("\\", "/"),
                MimeType = file.ContentType,
                FileSize = (int)file.Length,
                UploadDate = DateTime.Now,
            };

            _dbContext.fileData.Add(fileData);
            _dbContext.SaveChanges();

            return (fileData.Name, fileData.ID);
        }

        public (byte[] content, string mimeType, string fileName)? Download(int fileId)
        {
            var fileData = _dbContext.fileData.FirstOrDefault(f => f.ID == fileId);
            if (fileData == null)
                return null;

            var path = Path.Combine(_uploadPath, fileData.RelativePath);
            if (!System.IO.File.Exists(path))
                return null;

            var content = System.IO.File.ReadAllBytes(path);
            return (content, fileData.MimeType, fileData.Name);
        }
    }
}
