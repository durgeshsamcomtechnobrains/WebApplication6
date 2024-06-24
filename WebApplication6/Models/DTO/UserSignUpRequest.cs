namespace WebApplication6.Models.DTO
{
    public class UserSignUpRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public string ProfilePic { get; set; }
    }
}
