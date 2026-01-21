using BugReportApp.Contexts;
using BugReportApp.ModelDB;
using BugReportApp.Services.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.Remoting;
using BugReportApp.Model.Main;

namespace BugReportApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : Controller
    {
        private readonly IMainService _mainService;

        public MainController(IMainService mainService)
        {
            _mainService = mainService;
        }

        [HttpPost]
        [Route("getFullInfoAboutProject")]
        public IActionResult GetFullInfoAboutProject([FromBody] ProjectRequest model)
        {
            var project = _mainService.GetFullInfoAboutProject(model.ProjectId);
            if (project == null)
                return NotFound(new { message = "Project not found" });

            return Ok(new { message = "Project found", project });
        }

        [HttpPost]
        [Route("getTasks")]
        public IActionResult GetTasks([FromBody] TesterAndProjectDto model)
        {
            var tasks = _mainService.GetTasks(model.ProjectId, model.TesterId);
            if (!tasks.Any())
                return NotFound(new { message = "Tasks not found or user not in project" });

            return Ok(new { message = "Tasks found", tasks });
        }

        [HttpPost]
        [Route("chooseHeadTester")]
        public IActionResult ChooseHeadTester([FromBody] AddWorkerToProjectByHeadDeveloper model)
        {
            var error = _mainService.AddMainTesterToProject(model.Email, model.ProjectId);
            if (error != null)
                return BadRequest(new { message = error });

            return Ok(new { message = "worker added to project" });
        }

        [HttpPost]
        [Route("chooseDeveloper")]
        public IActionResult ChooseDeveloper([FromBody] AddWorkerToProjectByHeadDeveloper model)
        {
            var error = _mainService.AddDeveloperToProject(model.Email, model.ProjectId);
            if (error != null)
                return BadRequest(new { message = error });

            return Ok(new { message = "worker added to project" });
        }

        [HttpPost]
        [Route("chooseTester")]
        public IActionResult ChooseTester([FromBody] AddTesterToProject model)
        {
            var error = _mainService.AddTesterToProject(model.Email, model.ProjectId);
            if (error != null)
                return BadRequest(new { message = error });

            return Ok(new { message = "worker added to project" });
        }

        [HttpPost]
        [Route("getAllBugReportsForProject")]
        public IActionResult GetAllBugReportsForProject([FromBody] DataForBugReportsRequest model)
        {
            var bugsWithStatus = _mainService.GetAllBugReportsForProject(model.ProjectId, model.TesterId, model.DeveloperId);
            if (!bugsWithStatus.Any())
            {
                return Ok(new { message = "No bug reports found" });
            }

            var bugs = bugsWithStatus.Select(bws => new
            {
                Id = bws.Bug.ID,
                Title = bws.Bug.Title,
                Priority = bws.Bug.Priority,
                Severity = bws.Bug.Severity,
                TesterId = bws.Bug.TesterId,
                ProjectId = bws.Bug.ProjectId,
                CreatedAt = bws.Bug.CreatedAt,
                Status = bws.Status
            });

            return Ok(new { message = "Bugs with status", bugs });
        }
    }
}
