using BugReportApp.Model.Authentication;
using BugReportApp.ModelDB;
using Microsoft.AspNetCore.Identity;
using BugReportApp.Contexts;

namespace BugReportApp.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DBContext _context;
        private readonly PasswordHasher<UserData> _passwordHasher = new();

        public AuthenticationService(DBContext context)
        {
            _context = context;
        }

        public async Task<(bool, string, int?)> RegisterAsync(UserRegistrationData model)
        {
            if (_context.userData.Any(u => u.Email == model.Email))
                return (false, "Email already exists", null);

            var user = new UserData
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            _context.userData.Add(user);
            await _context.SaveChangesAsync();

            return (true, "User registered", user.ID);
        }

        public async Task<(bool, string, int?)> LoginAsync(UserLoginData model)
        {
            var user = _context.userData.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
                return (false, "Email does not exist", null);

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (result == PasswordVerificationResult.Failed)
                return (false, "Invalid password", null);

            return (true, "Login successful", user.ID);
        }
    }

}
