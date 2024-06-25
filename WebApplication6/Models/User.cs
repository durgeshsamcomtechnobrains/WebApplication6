using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class User : IdentityUser<string>    
    {
        //public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string ProfilePic { get; set; }
        //public bool Blocked { get; set; }
        //public List<int> BlockedUsers { get; set; } = new List<int>();
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
