namespace HWParts.Core.Domain.Commands
{
    public class ResetPasswordAccount : AccountCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }

        public ResetPasswordAccount()
        {

        }

        public ResetPasswordAccount(string email, string password, string code)
        {
            Email = email;
            Password = password;
            Code = code;
        }
    }
}