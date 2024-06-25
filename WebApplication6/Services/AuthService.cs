using Microsoft.AspNetCore.Identity;
using WebApplication6.Models;

namespace WebApplication6.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> SignUpAsync(User user)
        {
            // Implement signup logic here
            throw new NotImplementedException();
        }

        public async Task<User> LoginAsync(string userName, string password)
        {
            // Implement login logic here
            throw new NotImplementedException();
        }

        public async Task<bool> ResetPasswordAsync(string userName, string currentPassword, string newPassword)
        {
            // Implement reset password logic here
            throw new NotImplementedException();
        }
    }
}
