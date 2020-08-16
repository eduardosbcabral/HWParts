using MediatR;

namespace HWParts.Core.Domain.Commands
{
    public abstract class AccountCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
