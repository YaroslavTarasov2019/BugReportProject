using BugReportApp.Contexts;
using Microsoft.AspNetCore.Mvc;
using BugReportApp.ModelDB;
using BugReportApp.Services.File;

namespace BugReportApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] int userId)
        {
            try
            {
                var (fileName, fileId) = await _fileService.UploadAsync(file, userId);
                return Ok(new { fileName, fileId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("download/{fileId}")]
        public IActionResult DownloadFile(int fileId)
        {
            var result = _fileService.Download(fileId);
            if (result == null)
                return NotFound();

            var (content, mimeType, fileName) = result.Value;
            return File(content, mimeType, fileName);
        }

    }
}
