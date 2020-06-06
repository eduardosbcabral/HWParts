using HWParts.Core.Domain.Core.Commands;

namespace HWParts.Core.Domain.Commands
{
    public abstract class AccountCommand : Command
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
