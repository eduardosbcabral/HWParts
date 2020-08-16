using HWParts.Core.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.EventHandlers
{
    public class AccountRegisteredEventHandler :
        INotificationHandler<AccountRegisteredEvent>
    {
        public Task Handle(AccountRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
