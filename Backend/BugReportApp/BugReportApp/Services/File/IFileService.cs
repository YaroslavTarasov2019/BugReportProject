namespace BugReportApp.Services.File
{
    public interface IFileService
    {
        Task<(string fileName, int fileId)> UploadAsync(IFormFile file, int userId);
        (byte[] content, string mimeType, string fileName)? Download(int fileId);
    }
}
