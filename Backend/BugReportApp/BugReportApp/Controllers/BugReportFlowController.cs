using BugReportApp.Contexts;
using BugReportApp.ModelDB;
using BugReportApp.Services.BugReportFlow;
using Microsoft.AspNetCore.Mvc;
using BugReportApp.Model.BugReportFlow;

namespace BugReportApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugReportFlowController : Controller
    {
        private readonly IBugReportFlowService _service;

        public BugReportFlowController(IBugReportFlowService service)
        {
            _service = service;
        }

        [HttpPost("pass")]
        public async Task<IActionResult> PassBugReportOn([FromBody] BugReportPassOnRequest req)
        {
            var result = await _service.PassBugReportOnAsync(req.BugId, req.UserId);
            return result ? Ok(new { message = "Bug passed on" }) : BadRequest(new { message = "Invalid request" });
        }

        [HttpPost("reject")]
        public async Task<IActionResult> RejectBugReport([FromBody] BugReportRejectionRequest req)
        {
            var result = await _service.RejectBugReportAsync(req.BugId, req.UserId);
            return result ? Ok(new { message = "Bug rejected" }) : BadRequest(new { message = "Invalid request" });
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBugFix([FromBody] BugFixConfirmationRequest req)
        {
            var result = await _service.ConfirmBugFixAsync(req.BugId, req.TesterId, req.HeadTesterId);
            return result ? Ok(new { message = "Bug confirmed" }) : BadRequest(new { message = "Invalid request" });
        }

        [HttpPost("not-confirm")]
        public async Task<IActionResult> NotConfirmBugFix([FromBody] BugReportRejectionRequest req)
        {
            var result = await _service.NotConfirmBugFixAsync(req.BugId, req.UserId);
            return result ? Ok(new { message = "Bug not confirmed" }) : BadRequest(new { message = "Invalid request" });
        }

        [HttpPost("assign-developer")]
        public async Task<IActionResult> AssignDeveloper([FromBody] BugReportDeveloperAssignmentRequest req)
        {
            var result = await _service.AssignDeveloperAsync(req.BugId, req.DeveloperId, req.HeadDeveloperId);
            return result ? Ok(new { message = "Developer assigned" }) : BadRequest(new { message = "Invalid request" });
        }
    }

    
}
