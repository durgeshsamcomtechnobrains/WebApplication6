using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Conversation
    {
        public string Id { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }
        
        //public Guid id { get; set; }

        //public ICollection<Participant> participants { get; set; }

        //public ICollection<Message> messages { get; set; }
    }

    public class Participant
    {

        public string Id { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public User User { get; set; }
        public Conversation Conversation { get; set; }

        //[Key]
        //public Guid Id { get; set; }

        //[ForeignKey("Conversation")]
        //public Guid ConversationId { get; set; }

        //[ForeignKey("User")]
        //public Guid UserId { get; set; }
    }
}
