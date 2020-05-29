using HWParts.Core.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.EventHandlers
{
    public class PowerSupplyEventHandler :
        INotificationHandler<PowerSupplyRegisteredEvent>,
        INotificationHandler<PowerSupplyUpdatedEvent>,
        INotificationHandler<PowerSupplyRemovedEvent>
    {

        public Task Handle(PowerSupplyRegisteredEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PowerSupplyUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PowerSupplyRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
