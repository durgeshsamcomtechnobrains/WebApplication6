using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models.DTO;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                var result = await _authService.SignUpAsync(user);
                if (result == null)
                    return BadRequest(new { error = "Passwords didn't match" });

                return CreatedAtAction(nameof(SignUp), result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Something went wrong" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request.UserName, request.Password);
                if (result == null)
                    return BadRequest(new { error = "Invalid username or password" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Something went wrong. Please try again." });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // Implement logout logic here (if needed)
                return Ok(new { message = "Logout successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var result = await _authService.ResetPasswordAsync(request.UserName, request.Password, request.NewPassword);
                if (!result)
                    return BadRequest(new { error = "Reset password failed" });

                return Ok(new { message = "Password reset successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Something went wrong. Please try again." });
            }
        }
    }
}
