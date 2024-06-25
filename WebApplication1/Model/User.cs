using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }        

        [Required]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; }

        [Required]
        public string Gender { get; set; }

        public string ProfilePic { get; set; } = string.Empty;

        public bool Blocked { get; set; } = false;

        public ICollection<User> BlockedUsers { get; set; } = new List<User>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
