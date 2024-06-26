using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Model.DTO_s;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagesController(AppDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpPost("{senderId}/{receiverId}")]
        public async Task<IActionResult> SendMessage(string senderId, string receiverId, [FromBody] MessageDto messageDto)
        {
            // Ensure senderId is validated or authenticated properly
            var authenticatedSenderId = HttpContext.User.Identity.Name;
            if (senderId == "")
            {
                return Unauthorized("User is not authenticated or senderId mismatch.");
            }


            var conversation = await _context.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Participants.Any(p => p.UserId == senderId) && c.Participants.Any(p => p.UserId == receiverId));

            try
            {
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

                var message = new Message
                {
                    SenderId = Guid.Parse(senderId),
                    ReceiverId = Guid.Parse(receiverId),
                    Content = messageDto.Content,
                    CreatedAt = DateTime.UtcNow
                };

                conversation.Messages.Add(message);
                await _context.SaveChangesAsync();

                var receiverConnectionId = ChatHub.GetUserConnectionId(receiverId);
                if (receiverConnectionId != null)
                {
                    await _hubContext.Clients.Client(receiverConnectionId).SendAsync("NewMessage", message);
                }

                return CreatedAtAction(nameof(GetMessages), new { id = message.Id }, message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userToChatId}")]
        public async Task<IActionResult> GetMessages(string userToChatId)
        {
            var senderId = HttpContext.User.Identity.Name;
            if (senderId == null)
            {
                return Unauthorized();
            }

            var conversation = await _context.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Participants.Any(p => p.UserId == senderId) && c.Participants.Any(p => p.UserId == userToChatId));

            if (conversation == null)
            {
                return Ok(new List<Message>());
            }

            return Ok(conversation.Messages);
        }

        [HttpGet("last-messages")]
        public async Task<IActionResult> GetLastMessages()
        {
            var userId = HttpContext.User.Identity.Name;
            if (userId == null)
            {
                return Unauthorized();
            }

            var conversations = await _context.Conversations
                .Include(c => c.Messages.OrderByDescending(m => m.CreatedAt).Take(1))
                .Where(c => c.Participants.Any(p => p.UserId == userId))
                .ToListAsync();

            var lastMessages = conversations.Select(c => new
            {
                ConversationId = c.Id,
                LastMessage = c.Messages.FirstOrDefault(),
                Participants = c.Participants
            });

            return Ok(lastMessages);
        }
    }



    //[ApiController]
    //[Route("api/[controller]")]
    //[EnableCors]
    //public class MessageController : ControllerBase
    //{
    //    private readonly AppDbContext _context;

    //    public MessageController(AppDbContext context)
    //    {
    //        _context = context;
    //    }

    //    [HttpPost("{receiverId}")]
    //    public async Task<IActionResult> SendMessage(Guid receiverId, [FromBody] string content)
    //    {
    //        var senderId = Guid.Parse(User.Identity.Name); // Assuming the user ID is stored in the user identity

    //        var conversation = await _context.Conversations
    //            .Include(c => c.Participants)
    //            .FirstOrDefaultAsync(c => c.Participants.Any(p => p.UserId == senderId) &&
    //                                       c.Participants.Any(p => p.UserId == receiverId));

    //        if (conversation == null)
    //        {
    //            conversation = new Conversation
    //            {
    //                Participants = new List<Participant>
    //                {
    //                    new Participant { UserId = senderId },
    //                    new Participant { UserId = receiverId }
    //                }
    //            };
    //            _context.Conversations.Add(conversation);
    //        }

    //        var message = new Model.Message
    //        {
    //            SenderId = senderId,
    //            ReceiverId = receiverId,
    //            Content = content
    //        };

    //        conversation.Messages.Add(message);

    //        await _context.SaveChangesAsync();

    //        // Notify the receiver via SignalR (implementation to be added)

    //        return CreatedAtAction(nameof(SendMessage), new { id = message.Id }, message);
    //    }

    //    [HttpGet("{userToChatId}")]
    //    public async Task<IActionResult> GetMessages(Guid userToChatId)
    //    {
    //        var senderId = Guid.Parse(User.Identity.Name);

    //        var conversation = await _context.Conversations
    //            .Include(c => c.Messages)
    //            .FirstOrDefaultAsync(c => c.Participants.Any(p => p.UserId == senderId) &&
    //                                       c.Participants.Any(p => p.UserId == userToChatId));

    //        if (conversation == null)
    //        {
    //            return Ok(new Message[0]);
    //        }

    //        var messages = conversation.Messages.OrderBy(m => m.CreatedAt);

    //        return Ok(messages);
    //    }
    //}
}
