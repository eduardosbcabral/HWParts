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
    public class ProcessorCommandHandler : CommandHandler,
        IRequestHandler<RegisterProcessorCommand, bool>,
        IRequestHandler<UpdateProcessorCommand, bool>,
        IRequestHandler<RemoveProcessorCommand, bool>,
        IRequestHandler<ImportProcessorsCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IProcessorRepository _processorRepository;
        private readonly HWPartsDbContext _context;

        public ProcessorCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IProcessorRepository processorRepository,
            HWPartsDbContext context)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _processorRepository = processorRepository;
            _context = context;
        }

        public Task<bool> Handle(RegisterProcessorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var processor = new Processor(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_processorRepository.GetByPlatformId(processor.ComponentBase.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _processorRepository.Add(processor);

            if (Commit())
            {
                Bus.RaiseEvent(new ProcessorRegisteredEvent(processor.Id, processor.ComponentBase.Brand, processor.ComponentBase.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateProcessorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_processorRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var processor = _processorRepository.GetByPlatformId(request.PlatformId);

            if (processor != null && processor.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            processor.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _processorRepository.Update(processor);

            if (Commit())
            {
                Bus.RaiseEvent(new ProcessorUpdatedEvent(processor.Id, processor.ComponentBase.Brand, processor.ComponentBase.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveProcessorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _processorRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new ProcessorRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public async Task<bool> Handle(ImportProcessorsCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var cases = new List<Processor>();

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

                    var processorEntity = new Processor(
                        brand,
                        model,
                        platformId,
                        imageUrlString,
                        url,
                        platform);

                    var existsOnDb = _context.Processors
                            .Any(x => x.ComponentBase.PlatformId == processorEntity.ComponentBase.PlatformId);

                    if (!existsOnDb)
                    {
                        var existsOnCurrentList = cases.Any(x => x.ComponentBase.PlatformId == processorEntity.ComponentBase.PlatformId);

                        if (!existsOnCurrentList)
                        {
                            cases.Add(processorEntity);
                        }
                    }
                    else
                    {
                        try
                        {
                            var processorFromDb = _context.Processors
                                .SingleOrDefault(x => x.ComponentBase.PlatformId == processorEntity.ComponentBase.PlatformId);

                            processorFromDb.Update(platformId, imageUrlString, url, platform, brand, model);
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
                await Bus.RaiseEvent(new ProcessorsImportedEvent());
            }

            return true;
        }
    }
}
