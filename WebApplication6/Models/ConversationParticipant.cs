namespace WebApplication6.Models
{
    public class ConversationParticipant
    {
        public int Id { get; set; }

        public int UserId { get; set; } // Foreign key to User

        public int ConversationId { get; set; } // Foreign key to Conversation

        // Navigation properties
        public User User { get; set; }

        public Conversation Conversation { get; set; }
    }
}
