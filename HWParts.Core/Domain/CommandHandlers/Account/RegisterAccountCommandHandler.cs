using HWParts.Core.Domain.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers.Account
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, bool>
    {
        public Task<bool> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
