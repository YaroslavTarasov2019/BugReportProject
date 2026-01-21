using BugReportApp.Model.Authentication;

namespace BugReportApp.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<(bool Success, string Message, int? UserId)> RegisterAsync(UserRegistrationData model);
        Task<(bool Success, string Message, int? UserId)> LoginAsync(UserLoginData model);
    }
}
