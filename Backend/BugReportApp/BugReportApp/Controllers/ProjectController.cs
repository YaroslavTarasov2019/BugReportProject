using BugReportApp.Contexts;
using BugReportApp.ModelDB;
using BugReportApp.Services.Project;
using Microsoft.AspNetCore.Mvc;
using BugReportApp.Model.Project;

namespace BugReportApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        [Route("selectProjects")]
        public IActionResult SelectProjectsByUser([FromBody] UserIdRequest model)
        {
            var projects = _projectService.GetProjectsByUser(model.UserId);
            return Ok(projects);
        }

    }
}
