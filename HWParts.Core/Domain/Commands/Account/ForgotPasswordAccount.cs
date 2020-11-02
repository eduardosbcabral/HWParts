namespace HWParts.Core.Domain.Commands
{
    public class ForgotPasswordAccount : AccountCommand
    {
        public string Email { get; set; }
        
        public ForgotPasswordAccount()
        {

        }

        public ForgotPasswordAccount(string email)
        {
            Email = email;
        }
    }
}
