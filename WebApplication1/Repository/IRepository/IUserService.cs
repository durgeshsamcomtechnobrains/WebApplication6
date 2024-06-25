using WebApplication1.Model.DTO_s;
using WebApplication1.Model;

namespace WebApplication1.Repository.IRepository
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string userName, string password);
        Task<UserRegistrationResult> RegisterUserAsync(UserRegistrationDto userDto);        
    }
}
