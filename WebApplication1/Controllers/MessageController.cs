using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Model.DTO_s;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors]
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{receiverId}")]
        public async Task<IActionResult> SendMessage(Guid receiverId, [FromBody] string content)
        {
            var senderId = Guid.Parse(User.Identity.Name); // Assuming the user ID is stored in the user identity

            var conversation = await _context.Conversations
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.Participants.Any(p => p.UserId == senderId) &&
                                           c.Participants.Any(p => p.UserId == receiverId));

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    Participants = new List<Participant>
                    {
                        new Participant { UserId = senderId },
                        new Participant { UserId = receiverId }
                    }
                };
                _context.Conversations.Add(conversation);
            }

            var message = new Model.Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
            };

            conversation.Messages.Add(message);

            await _context.SaveChangesAsync();

            // Notify the receiver via SignalR (implementation to be added)

            return CreatedAtAction(nameof(SendMessage), new { id = message.Id }, message);
        }

        [HttpGet("{userToChatId}")]
        public async Task<IActionResult> GetMessages(Guid userToChatId)
        {
            var senderId = Guid.Parse(User.Identity.Name);

            var conversation = await _context.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Participants.Any(p => p.UserId == senderId) &&
                                           c.Participants.Any(p => p.UserId == userToChatId));

            if (conversation == null)
            {
                return Ok(new Message[0]);
            }

            var messages = conversation.Messages.OrderBy(m => m.CreatedAt);

            return Ok(messages);
        }
    }
}
