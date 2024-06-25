using WebApplication6.Models;

namespace WebApplication6.Services
{
    public interface IAuthService
    {
        Task<User> SignUpAsync(User user);
        Task<User> LoginAsync(string userName, string password);
        Task<bool> ResetPasswordAsync(string userName, string currentPassword, string newPassword);
    }
}
