using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, CommandResponse>
    {
        public async Task<CommandResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            return new SuccessCommandResponse();
        }
    }
}
