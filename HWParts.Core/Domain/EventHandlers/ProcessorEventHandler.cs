using HWParts.Core.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.EventHandlers
{
    public class ProcessorEventHandler :
        INotificationHandler<ProcessorRegisteredEvent>,
        INotificationHandler<ProcessorUpdatedEvent>,
        INotificationHandler<ProcessorRemovedEvent>
    {

        public Task Handle(ProcessorRegisteredEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProcessorUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProcessorRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
