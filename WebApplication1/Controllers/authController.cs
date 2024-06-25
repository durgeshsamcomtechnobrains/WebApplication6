using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model.DTO_s;
using WebApplication1.Repository;
using WebApplication1.Repository.IRepository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class authController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ITokenService _tokenService;

        public authController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserRegistrationDto userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.ErrorMessage });
            }
            return CreatedAtAction(nameof(SignUp), new { id = result.User.Id }, result.User);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.AuthenticateAsync(loginDto.UserName, loginDto.Password);

            if (user == null)
                return Unauthorized();

            var jwtToken = _tokenService.GenerateJwtToken(user);

            return Ok(new
            {
                _id = user.Id,
                fullName = user.FullName,
                userName = user.UserName,
                profilePic = user.ProfilePic,
                JwtToken = jwtToken
            });
        }
    }
}
