using HWParts.Core.Domain.Bus;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Notifications;
using HWParts.Core.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class RegisterMemoryCommandHandler : CommandHandler,
        IRequestHandler<RegisterMemoryCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMemoryRepository _memoryRepository;

        public RegisterMemoryCommandHandler(
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
    }
}
