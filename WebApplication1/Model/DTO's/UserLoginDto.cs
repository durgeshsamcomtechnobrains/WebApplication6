namespace WebApplication1.Model.DTO_s
{
    public class UserLoginDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; } = string.Empty;
        public string Password { get; set; }       
    }
}
