//using HWParts.Core.Domain.Handlers.Shared;
//using HWParts.Core.Domain.Commands;
//using HWParts.Core.Domain.Core.Bus;
//using HWParts.Core.Domain.Core.Notifications;
//using HWParts.Core.Domain.Entities;
//using HWParts.Core.Domain.Enums;
//using HWParts.Core.Domain.Events;
//using HWParts.Core.Domain.Interfaces;
//using HWParts.Core.Infrastructure;
//using MediatR;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace HWParts.Core.Domain.Handlers
//{
//    public class MemoryCommandHandler : CommandHandler,
//        IRequestHandler<RegisterMemoryCommand, bool>,
//        IRequestHandler<UpdateMemoryCommand, bool>,
//        IRequestHandler<RemoveMemoryCommand, bool>,
//        IRequestHandler<ImportMemoriesCommand, bool>
//    {
//        private readonly IMediatorHandler Bus;
//        private readonly IMemoryRepository _memoryRepository;
//        private readonly HWPartsDbContext _context;

//        public MemoryCommandHandler(
//            IUnitOfWork uow,
//            IMediatorHandler bus,
//            INotificationHandler<DomainNotification> notifications,
//            IMemoryRepository memoryRepository,
//            HWPartsDbContext context)
//            : base(uow, bus, notifications)
//        {
//            Bus = bus;
//            _memoryRepository = memoryRepository;
//            _context = context;
//        }

//        public Task<bool> Handle(RegisterMemoryCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            var memory = new Memory(
//                request.Brand,
//                request.Model,
//                request.PlatformId,
//                request.ImageUrl,
//                request.Url,
//                request.Platform);

//            if (_memoryRepository.GetByPlatformId(memory.PlatformId) != null)
//            {
//                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
//                return Task.FromResult(false);
//            }

//            _memoryRepository.Add(memory);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new MemoryRegisteredEvent(memory.Id, memory.Brand, memory.Model));
//            }

//            return Task.FromResult(true);
//        }

//        public Task<bool> Handle(UpdateMemoryCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            if (!_memoryRepository.Exists(request.Id))
//            {
//                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
//                return Task.FromResult(false);
//            }

//            var memory = _memoryRepository.GetByPlatformId(request.PlatformId);

//            if (memory != null && memory.Id != request.Id)
//            {
//                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
//                return Task.FromResult(false);
//            }

//            memory.Update(
//                request.PlatformId,
//                request.ImageUrl,
//                request.Url,
//                request.Platform,
//                request.Brand,
//                request.Model);

//            _memoryRepository.Update(memory);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new MemoryUpdatedEvent(memory.Id, memory.Brand, memory.Model));
//            }

//            return Task.FromResult(true);
//        }

//        public Task<bool> Handle(RemoveMemoryCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            _memoryRepository.Remove(request.Id);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new MemoryRemovedEvent(request.Id));
//            }

//            return Task.FromResult(true);
//        }

//        public async Task<bool> Handle(ImportMemoriesCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return false;
//            }

//            var cases = new List<Memory>();

//            using (var reader = new StreamReader(request.File.OpenReadStream()))
//            {
//                var json = await reader.ReadToEndAsync();

//                var componentsDeserialized = JsonConvert.DeserializeObject<List<dynamic>>(json);

//                foreach (var item in componentsDeserialized)
//                {
//                    var brand = ImportFileHelper.BindParameter<string>("Brand", item);
//                    var model = ImportFileHelper.BindParameter<string>("Model", item);
//                    var platformId = ImportFileHelper.BindParameter<string>("platform_id", item);
//                    var imagesUrls = (ImportFileHelper.BindParameter<List<string>>("images_urls", item));
//                    var url = ImportFileHelper.BindParameter<string>("url", item);
//                    var platform = (EPlatform)Enum.Parse(typeof(EPlatform), ImportFileHelper.BindParameter<string>("platform", item), true);

//                    var imageUrlString = string.Join(";", imagesUrls);

//                    var memoryEntity = new Memory(
//                        brand,
//                        model,
//                        platformId,
//                        imageUrlString,
//                        url,
//                        platform);

//                    var existsOnDb = _context.Memories
//                            .Any(x => x.PlatformId == memoryEntity.PlatformId);

//                    if (!existsOnDb)
//                    {
//                        var existsOnCurrentList = cases.Any(x => x.PlatformId == memoryEntity.PlatformId);

//                        if (!existsOnCurrentList)
//                        {
//                            cases.Add(memoryEntity);
//                        }
//                    }
//                    else
//                    {
//                        try
//                        {
//                            var memoriesFromDb = _context.Memories
//                                .SingleOrDefault(x => x.PlatformId == memoryEntity.PlatformId);

//                            memoriesFromDb.Update(platformId, imageUrlString, url, platform, brand, model);
//                        }
//                        catch (Exception)
//                        {
//                            await Bus.RaiseEvent(new DomainNotification(request.MessageType, "Ocorreu um erro ao importar o arquivo."));
//                            return false;
//                        }
//                    }
//                }
//            }

//            await _context.AddRangeAsync(cases);

//            if (Commit())
//            {
//                await Bus.RaiseEvent(new MemoriesImportedEvent());
//            }

//            return true;
//        }
//    }
//}
