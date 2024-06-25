using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Model.DTO_s;
using WebApplication1.Repository.IRepository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("sidebar")]
        [Authorize]
        public async Task<IActionResult> GetUsersForSidebar()
        {
            try
            {
                var loggedUserId = new Guid(User.Identity.Name); // Assuming the logged-in user's ID is stored in the Name claim
                var loggedUser = await _userRepository.GetUserByIdAsync(loggedUserId);
                var users = await _userRepository.GetUsersExceptAsync(loggedUserId);

                var userDtos = users.Select(user => new UserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Gender = user.Gender,
                    ProfilePic = user.ProfilePic,                                        
                });

                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}
