using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using WebApplication1.Model.DTO_s;
using WebApplication1.Model;
using WebApplication1.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<UserRegistrationResult> RegisterUserAsync(UserRegistrationDto userDto)
        {
            if (userDto.Password != userDto.ConfirmPassword)
            {
                return new UserRegistrationResult { IsSuccess = false, ErrorMessage = "Passwords didn't match" };
            }

            if (await _context.Users.AnyAsync(u => u.UserName == userDto.UserName))
            {
                return new UserRegistrationResult { IsSuccess = false, ErrorMessage = "Username already exists" };
            }

            var newUser = new User
            {
                FullName = userDto.FullName,
                UserName = userDto.UserName,
                Gender = userDto.Gender,
                ProfilePic = userDto.Gender == "male"
                    ? $"https://avatar.iran.liara.run/public/boy?username={userDto.UserName}"
                    : $"https://avatar.iran.liara.run/public/girl?username={userDto.UserName}"
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, userDto.Password);

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                _tokenService.GenerateTokenSetCookies(newUser.Id, httpContext.Response);
            }

            return new UserRegistrationResult { IsSuccess = true, User = newUser };
        }
    }
}
