using HWParts.Core.Domain.Validations;

namespace HWParts.Core.Domain.Commands
{
    public class LoginAccountCommand : AccountCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
