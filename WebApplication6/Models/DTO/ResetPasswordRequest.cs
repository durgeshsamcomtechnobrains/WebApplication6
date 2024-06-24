namespace WebApplication6.Models.DTO
{
    public class ResetPasswordRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
