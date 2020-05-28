using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class StorageCommandHandler : CommandHandler,
        IRequestHandler<RegisterStorageCommand, bool>,
        IRequestHandler<UpdateStorageCommand, bool>,
        IRequestHandler<RemoveStorageCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IStorageRepository _storageRepository;

        public StorageCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IStorageRepository storageRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _storageRepository = storageRepository;
        }

        public Task<bool> Handle(RegisterStorageCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var storage = new Storage(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_storageRepository.GetByPlatformId(storage.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _storageRepository.Add(storage);

            if (Commit())
            {
                Bus.RaiseEvent(new StorageRegisteredEvent(storage.Id, storage.Brand, storage.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_storageRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var storage = _storageRepository.GetByPlatformId(request.PlatformId);

            if (storage != null && storage.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            storage.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _storageRepository.Update(storage);

            if (Commit())
            {
                Bus.RaiseEvent(new StorageUpdatedEvent(storage.Id, storage.Brand, storage.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveStorageCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _storageRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new StorageRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }
    }
}
