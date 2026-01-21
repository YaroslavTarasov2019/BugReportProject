using Microsoft.AspNetCore.Mvc;
using BugReportApp.Model.Authentication;
using BugReportApp.Services.Authentication;

namespace BugReportApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationData model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result.Success)
                return Conflict(new { message = result.Message });

            return Ok(new { message = result.Message, id = result.UserId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginData model)
        {
            var result = await _authService.LoginAsync(model);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message, id = result.UserId });
        }
    }
}
