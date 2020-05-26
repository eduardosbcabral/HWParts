using HWParts.Core.Domain.Bus;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Notifications;
using HWParts.Core.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class RemoveMemoryCommandHandler : CommandHandler,
        IRequestHandler<RemoveMemoryCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMemoryRepository _memoryRepository;

        public RemoveMemoryCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IMemoryRepository memoryRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;

            _memoryRepository = memoryRepository;
        }

        public Task<bool> Handle(RemoveMemoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _memoryRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new MemoryRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }
    }
}

