using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class MemoryCommandHandler : CommandHandler,
        IRequestHandler<RegisterMemoryCommand, bool>,
        IRequestHandler<UpdateMemoryCommand, bool>,
        IRequestHandler<RemoveMemoryCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMemoryRepository _memoryRepository;

        public MemoryCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IMemoryRepository memoryRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _memoryRepository = memoryRepository;
        }

        public Task<bool> Handle(RegisterMemoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var memory = new Memory(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_memoryRepository.GetByPlatformId(memory.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _memoryRepository.Add(memory);

            if (Commit())
            {
                Bus.RaiseEvent(new MemoryRegisteredEvent(memory.Id, memory.Brand, memory.Model));
            }

            return Task.FromResult(true);
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
