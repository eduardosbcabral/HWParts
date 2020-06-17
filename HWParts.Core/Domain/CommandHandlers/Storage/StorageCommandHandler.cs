using HWParts.Core.Domain.CommandHandlers.Shared;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class StorageCommandHandler : CommandHandler,
        IRequestHandler<RegisterStorageCommand, bool>,
        IRequestHandler<UpdateStorageCommand, bool>,
        IRequestHandler<RemoveStorageCommand, bool>,
        IRequestHandler<ImportStoragesCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IStorageRepository _storageRepository;
        private readonly HWPartsDbContext _context;

        public StorageCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IStorageRepository storageRepository,
            HWPartsDbContext context)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _storageRepository = storageRepository;
            _context = context;
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

            if (_storageRepository.GetByPlatformId(storage.ComponentBase.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _storageRepository.Add(storage);

            if (Commit())
            {
                Bus.RaiseEvent(new StorageRegisteredEvent(storage.Id, storage.ComponentBase.Brand, storage.ComponentBase.Model));
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
                Bus.RaiseEvent(new StorageUpdatedEvent(storage.Id, storage.ComponentBase.Brand, storage.ComponentBase.Model));
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

        public async Task<bool> Handle(ImportStoragesCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var cases = new List<Storage>();

            using (var reader = new StreamReader(request.File.OpenReadStream()))
            {
                var json = await reader.ReadToEndAsync();

                var componentsDeserialized = JsonConvert.DeserializeObject<List<dynamic>>(json);

                foreach (var item in componentsDeserialized)
                {
                    var brand = ImportFileHelper.BindParameter<string>("Brand", item);
                    var model = ImportFileHelper.BindParameter<string>("Model", item);
                    var platformId = ImportFileHelper.BindParameter<string>("platform_id", item);
                    var imagesUrls = (ImportFileHelper.BindParameter<List<string>>("images_urls", item));
                    var url = ImportFileHelper.BindParameter<string>("url", item);
                    var platform = (EPlatform)Enum.Parse(typeof(EPlatform), ImportFileHelper.BindParameter<string>("platform", item), true);

                    var imageUrlString = string.Join(";", imagesUrls);

                    var storageEntity = new Storage(
                        brand,
                        model,
                        platformId,
                        imageUrlString,
                        url,
                        platform);

                    var existsOnDb = _context.Storages
                            .Any(x => x.ComponentBase.PlatformId == storageEntity.ComponentBase.PlatformId);

                    if (!existsOnDb)
                    {
                        var existsOnCurrentList = cases.Any(x => x.ComponentBase.PlatformId == storageEntity.ComponentBase.PlatformId);

                        if (!existsOnCurrentList)
                        {
                            cases.Add(storageEntity);
                        }
                    }
                    else
                    {
                        try
                        {
                            var storageFromDb = _context.Storages
                                .SingleOrDefault(x => x.ComponentBase.PlatformId == storageEntity.ComponentBase.PlatformId);

                            storageFromDb.Update(platformId, imageUrlString, url, platform, brand, model);
                        }
                        catch (Exception)
                        {
                            await Bus.RaiseEvent(new DomainNotification(request.MessageType, "Ocorreu um erro ao importar o arquivo."));
                            return false;
                        }
                    }
                }
            }

            await _context.AddRangeAsync(cases);

            if (Commit())
            {
                await Bus.RaiseEvent(new StoragesImportedEvent());
            }

            return true;
        }
    }
}
