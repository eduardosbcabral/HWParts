using HWParts.Core.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.EventHandlers
{
    public class MotherboardEventHandler :
        INotificationHandler<MotherboardRegisteredEvent>,
        INotificationHandler<MotherboardUpdatedEvent>,
        INotificationHandler<MotherboardRemovedEvent>
    {

        public Task Handle(MotherboardRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(MotherboardUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(MotherboardRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
