using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model.DTO_s;
using WebApplication1.Repository.IRepository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class authController : ControllerBase
    {
        private readonly IUserService _userService;

        public authController(IUserService userService)
        {
            _userService = userService;
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
    }
}
