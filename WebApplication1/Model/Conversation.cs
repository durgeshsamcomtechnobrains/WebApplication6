using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; }

        public ICollection<Participant> Participants { get; set; }

        public ICollection<Message> Messages { get; set; }
    }

    public class Participant
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Conversation")]
        public Guid ConversationId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}
