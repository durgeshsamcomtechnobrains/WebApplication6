namespace WebApplication6.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        public ICollection<int> Participants { get; set; } = new List<int>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
