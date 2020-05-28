using HWParts.Core.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.EventHandlers
{
    public class StorageEventHandler :
        INotificationHandler<StorageRegisteredEvent>,
        INotificationHandler<StorageUpdatedEvent>,
        INotificationHandler<StorageRemovedEvent>
    {

        public Task Handle(StorageRegisteredEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(StorageUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(StorageRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
