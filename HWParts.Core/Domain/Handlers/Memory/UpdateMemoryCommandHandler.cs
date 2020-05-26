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
    public class UpdateMemoryCommandHandler : CommandHandler,
        IRequestHandler<UpdateMemoryCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMemoryRepository _memoryRepository;

        public UpdateMemoryCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IMemoryRepository memoryRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _memoryRepository = memoryRepository;
        }

        public Task<bool> Handle(UpdateMemoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_memoryRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var memory = _memoryRepository.GetByPlatformId(request.PlatformId);

            if (memory != null && memory.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            memory.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _memoryRepository.Update(memory);

            if (Commit())
            {
                Bus.RaiseEvent(new MemoryUpdatedEvent(memory.Id, memory.Brand, memory.Model));
            }

            return Task.FromResult(true);
        }
    }
}

