using BugReportApp.Contexts;
using BugReportApp.ModelDB;
using BugReportApp.Services.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using BugReportApp.Model.Form;

namespace BugReportApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : Controller
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpPost("createProject")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto model)
        {
            if (await _formService.ProjectExistsAsync(model.Title, model.UserId))
                return Conflict(new { message = "Project already exist" });

            await _formService.CreateProjectAsync(model);
            return Ok(new { message = "Project created" });
        }

        [HttpPost("createBugReport")]
        public async Task<IActionResult> CreateBugReport([FromBody] BugReportCreateDto model)
        {
            if (!await _formService.ProjectExistsByIdAsync(model.ProjectID))
                return NotFound(new { message = "Project not exist" });

            if (!await _formService.UserExistsAsync(model.UserID))
                return NotFound(new { message = "User not exist" });

            await _formService.CreateBugReportAsync(model);
            return Ok(new { message = "Bug report created" });
        }

        [HttpPost("getUserNameAndSurname")]
        public async Task<IActionResult> GetUserNameAndSurname([FromBody] UserIdRequestDto model)
        {
            var user = await _formService.GetUserByIdAsync(model.UserID);
            if (user == null)
                return NotFound(new { message = "User not exist" });

            return Ok(new { message = "User Data gets", user });
        }

        [HttpPost("getProjectTitle")]
        public async Task<IActionResult> GetProjectTitle([FromBody] ProjectIdRequestDto model)
        {
            var project = await _formService.GetProjectByIdAsync(model.ProjectID);
            if (project == null)
                return NotFound(new { message = "Project not exist" });

            return Ok(new { message = "Project Data gets", project });
        }
    }
}
