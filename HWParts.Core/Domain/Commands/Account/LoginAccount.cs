namespace HWParts.Core.Domain.Commands
{
    public class LoginAccount : AccountCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public LoginAccount()
        {

        }

        public LoginAccount(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
