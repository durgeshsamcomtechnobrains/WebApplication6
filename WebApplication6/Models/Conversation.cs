using System.Collections.Generic;
namespace WebApplication6.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        public ICollection<ConversationParticipant> Participants { get; set; } = new List<ConversationParticipant>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();        

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;        
    }
}
