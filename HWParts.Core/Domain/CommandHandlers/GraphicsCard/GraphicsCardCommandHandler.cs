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
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class GraphicsCardCommandHandler : CommandHandler,
        IRequestHandler<RegisterGraphicsCardCommand, bool>,
        IRequestHandler<UpdateGraphicsCardCommand, bool>,
        IRequestHandler<RemoveGraphicsCardCommand, bool>,
        IRequestHandler<ImportGraphicsCardsCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IGraphicsCardRepository _graphicsCardRepository;
        private readonly HWPartsDbContext _context;

        public GraphicsCardCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IGraphicsCardRepository graphicsCardRepository,
            HWPartsDbContext context)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _graphicsCardRepository = graphicsCardRepository;
            _context = context;
        }

        public Task<bool> Handle(RegisterGraphicsCardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var graphicsCard = new GraphicsCard(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_graphicsCardRepository.GetByPlatformId(graphicsCard.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _graphicsCardRepository.Add(graphicsCard);

            if (Commit())
            {
                Bus.RaiseEvent(new GraphicsCardRegisteredEvent(graphicsCard.Id, graphicsCard.Brand, graphicsCard.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateGraphicsCardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_graphicsCardRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var graphicsCard = _graphicsCardRepository.GetByPlatformId(request.PlatformId);

            if (graphicsCard != null && graphicsCard.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            graphicsCard.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _graphicsCardRepository.Update(graphicsCard);

            if (Commit())
            {
                Bus.RaiseEvent(new GraphicsCardUpdatedEvent(graphicsCard.Id, graphicsCard.Brand, graphicsCard.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveGraphicsCardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _graphicsCardRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new GraphicsCardRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public async Task<bool> Handle(ImportGraphicsCardsCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var cases = new List<GraphicsCard>();

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

                    var graphicsCardEntity = new GraphicsCard(
                        brand,
                        model,
                        platformId,
                        imageUrlString,
                        url,
                        platform);

                    var existsOnDb = _context.GraphicsCards
                            .Any(x => x.PlatformId == graphicsCardEntity.PlatformId);

                    if (!existsOnDb)
                    {
                        var existsOnCurrentList = cases.Any(x => x.PlatformId == graphicsCardEntity.PlatformId);

                        if (!existsOnCurrentList)
                        {
                            cases.Add(graphicsCardEntity);
                        }
                    }
                    else
                    {
                        try
                        {
                            var graphicsCardsFromDb = _context.GraphicsCards
                                .SingleOrDefault(x => x.PlatformId == graphicsCardEntity.PlatformId);

                            graphicsCardsFromDb.Update(platformId, imageUrlString, url, platform, brand, model);
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
                await Bus.RaiseEvent(new GraphicsCardsImportedEvent());
            }

            return true;
        }
    }
}
