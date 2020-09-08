using MediatR;

namespace HWParts.Core.Domain.Core.Commands
{
    public abstract class CommandBase : IRequest<CommandResponse>
    {
    }
}
