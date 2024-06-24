using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        public string ProfilePic { get; set; }

        public bool Blocked { get; set; }

        // Navigation property for blocked users
        public ICollection<User> BlockedUsers { get; set; }
    }
}
