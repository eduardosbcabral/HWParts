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
    public class MotherboardCommandHandler : CommandHandler,
        IRequestHandler<RegisterMotherboardCommand, bool>,
        IRequestHandler<UpdateMotherboardCommand, bool>,
        IRequestHandler<RemoveMotherboardCommand, bool>,
        IRequestHandler<ImportMotherboardsCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMotherboardRepository _motherboardRepository;
        private readonly HWPartsDbContext _context;

        public MotherboardCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IMotherboardRepository motherboardRepository,
            HWPartsDbContext context)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _motherboardRepository = motherboardRepository;
            _context = context;
        }

        public Task<bool> Handle(RegisterMotherboardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var motherboard = new Motherboard(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_motherboardRepository.GetByPlatformId(motherboard.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _motherboardRepository.Add(motherboard);

            if (Commit())
            {
                Bus.RaiseEvent(new MotherboardRegisteredEvent(motherboard.Id, motherboard.Brand, motherboard.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateMotherboardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_motherboardRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var motherboard = _motherboardRepository.GetByPlatformId(request.PlatformId);

            if (motherboard != null && motherboard.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            motherboard.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _motherboardRepository.Update(motherboard);

            if (Commit())
            {
                Bus.RaiseEvent(new MotherboardUpdatedEvent(motherboard.Id, motherboard.Brand, motherboard.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveMotherboardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _motherboardRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new MotherboardRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public async Task<bool> Handle(ImportMotherboardsCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var cases = new List<Motherboard>();

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

                    var motherboardEntity = new Motherboard(
                        brand,
                        model,
                        platformId,
                        imageUrlString,
                        url,
                        platform);

                    var existsOnDb = _context.Motherboards
                            .Any(x => x.PlatformId == motherboardEntity.PlatformId);

                    if (!existsOnDb)
                    {
                        var existsOnCurrentList = cases.Any(x => x.PlatformId == motherboardEntity.PlatformId);

                        if (!existsOnCurrentList)
                        {
                            cases.Add(motherboardEntity);
                        }
                    }
                    else
                    {
                        try
                        {
                            var motherboardFromDb = _context.Motherboards
                                .SingleOrDefault(x => x.PlatformId == motherboardEntity.PlatformId);

                            motherboardFromDb.Update(platformId, imageUrlString, url, platform, brand, model);
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
                await Bus.RaiseEvent(new MotherboardsImportedEvent());
            }

            return true;
        }
    }
}
