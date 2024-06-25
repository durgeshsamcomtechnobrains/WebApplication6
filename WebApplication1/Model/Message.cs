using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Message 
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid SenderId { get; set; }

        [ForeignKey("User")]
        public Guid ReceiverId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
