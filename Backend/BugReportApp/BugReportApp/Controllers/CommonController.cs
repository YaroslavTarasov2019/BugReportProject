using BugReportApp.Contexts;
using BugReportApp.ModelDB;
using BugReportApp.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BugReportApp.Model.Common;

namespace BugReportApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : Controller
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpPost("getStatisticsInformation")]
        public IActionResult GetStatisticsInformation([FromBody] RoleAndProjectRequest model)
        {
            if (model.UserRole == "HEAD_DEVELOPER" ||  model.UserRole == "HEAD_TESTER")
            { 
                var stats = _commonService.GetStatisticsInformationHead(model);
                return Ok(new { message = "this is your data", stats = stats });
            }
            else if (model.UserRole == "DEVELOPER")
            {
                var stats = _commonService.GetStatisticsInformationDeveloper(model);
                return Ok(new { message = "this is your data", stats = stats });
            }
            else if (model.UserRole == "TESTER")
            {
                var stats = _commonService.GetStatisticsInformationTester(model);
                return Ok(new { message = "this is your data", stats = stats });
            }
            else
                return BadRequest();
        }

        [HttpGet("getTestersByProject")]
        public IActionResult GetTestersByProject([FromQuery] int projectId)
        {
            var testers = _commonService.GetTestersByProject(projectId);
            if (testers.Count == 0)
                return BadRequest(new { message = "No testers found for this project." });

            return Ok(new { message = "this is your data", testers });
        }

        [HttpPost("getBugById")]
        public IActionResult GetBugById([FromBody] BugIdRequest model)
        {
            var (bug, history) = _commonService.GetBugById(model.BugId);
            if (bug == null)
                return BadRequest(new { message = "No bug-report found for this project." });

            return Ok(new { message = "this is your data", bugData = bug, bugHistoryData = history });
        }

        [HttpPost("getDeveloperByIdProject")]
        public IActionResult GetDeveloperByIdProject([FromBody] ProjectIdRequest model)
        {
            var developers = _commonService.GetDevelopersByProjectId(model.ProjectId);
            if (developers.Count == 0)
                return BadRequest(new { message = "No user found for this project." });

            return Ok(new { message = "confirm bug fix by head tester", developers });
        }

        [HttpPost("assignTasks")]
        public IActionResult AssignTasks([FromBody] TaskAssignment model)
        {
            _commonService.AssignTask(model);
            return Ok(new { message = "this is your data" });
        }
    }
}
