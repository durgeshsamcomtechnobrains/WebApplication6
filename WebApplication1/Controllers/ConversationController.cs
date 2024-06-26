using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors]
    public class ConversationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConversationController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet("lastMessages")]
        //public async Task<IActionResult> GetLastMessages()
        //{
        //    var userId = Guid.Parse(User.Identity.Name);

        //    var conversations = await _context.Conversations
        //        .Include(c => c.Messages.OrderByDescending(m => m.CreatedAt).Take(1))
        //        .Where(c => c.Participants.Any(p => p.UserId == userId))
        //        .ToListAsync();

        //    var lastMessages = conversations.Select(c => new
        //    {
        //        ConversationId = c.Id,
        //        LastMessage = c.Messages.FirstOrDefault(),
        //        Participants = c.Participants
        //    });

        //    return Ok(lastMessages);
        //}
    }
}
