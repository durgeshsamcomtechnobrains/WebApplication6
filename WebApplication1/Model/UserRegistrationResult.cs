namespace WebApplication1.Model
{
    public class UserRegistrationResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public User User { get; set; }
    }
}
