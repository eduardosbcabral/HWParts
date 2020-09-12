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
//    public class PowerSupplyCommandHandler : CommandHandler,
//        IRequestHandler<RegisterPowerSupplyCommand, bool>,
//        IRequestHandler<UpdatePowerSupplyCommand, bool>,
//        IRequestHandler<RemovePowerSupplyCommand, bool>,
//        IRequestHandler<ImportPowerSuppliesCommand, bool>
//    {
//        private readonly IMediatorHandler Bus;
//        private readonly IPowerSupplyRepository _powerSupplyRepository;
//        private readonly HWPartsDbContext _context;

//        public PowerSupplyCommandHandler(
//            IUnitOfWork uow,
//            IMediatorHandler bus,
//            INotificationHandler<DomainNotification> notifications,
//            IPowerSupplyRepository powerSupplyRepository,
//            HWPartsDbContext context)
//            : base(uow, bus, notifications)
//        {
//            Bus = bus;
//            _powerSupplyRepository = powerSupplyRepository;
//            _context = context;
//        }

//        public Task<bool> Handle(RegisterPowerSupplyCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            var powerSupply = new PowerSupply(
//                request.Brand,
//                request.Model,
//                request.PlatformId,
//                request.ImageUrl,
//                request.Url,
//                request.Platform);

//            if (_powerSupplyRepository.GetByPlatformId(powerSupply.PlatformId) != null)
//            {
//                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
//                return Task.FromResult(false);
//            }

//            _powerSupplyRepository.Add(powerSupply);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new PowerSupplyRegisteredEvent(powerSupply.Id, powerSupply.Brand, powerSupply.Model));
//            }

//            return Task.FromResult(true);
//        }

//        public Task<bool> Handle(UpdatePowerSupplyCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            if (!_powerSupplyRepository.Exists(request.Id))
//            {
//                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
//                return Task.FromResult(false);
//            }

//            var powerSupply = _powerSupplyRepository.GetByPlatformId(request.PlatformId);

//            if (powerSupply != null && powerSupply.Id != request.Id)
//            {
//                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
//                return Task.FromResult(false);
//            }

//            powerSupply.Update(
//                request.PlatformId,
//                request.ImageUrl,
//                request.Url,
//                request.Platform,
//                request.Brand,
//                request.Model);

//            _powerSupplyRepository.Update(powerSupply);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new PowerSupplyUpdatedEvent(powerSupply.Id, powerSupply.Brand, powerSupply.Model));
//            }

//            return Task.FromResult(true);
//        }

//        public Task<bool> Handle(RemovePowerSupplyCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            _powerSupplyRepository.Remove(request.Id);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new PowerSupplyRemovedEvent(request.Id));
//            }

//            return Task.FromResult(true);
//        }

//        public async Task<bool> Handle(ImportPowerSuppliesCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return false;
//            }

//            var cases = new List<PowerSupply>();

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

//                    var powerSupplyEntity = new PowerSupply(
//                        brand,
//                        model,
//                        platformId,
//                        imageUrlString,
//                        url,
//                        platform);

//                    var existsOnDb = _context.PowerSupplies
//                            .Any(x => x.PlatformId == powerSupplyEntity.PlatformId);

//                    if (!existsOnDb)
//                    {
//                        var existsOnCurrentList = cases.Any(x => x.PlatformId == powerSupplyEntity.PlatformId);

//                        if (!existsOnCurrentList)
//                        {
//                            cases.Add(powerSupplyEntity);
//                        }
//                    }
//                    else
//                    {
//                        try
//                        {
//                            var powerSupplyFromDb = _context.PowerSupplies
//                                .SingleOrDefault(x => x.PlatformId == powerSupplyEntity.PlatformId);

//                            powerSupplyFromDb.Update(platformId, imageUrlString, url, platform, brand, model);
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
//                await Bus.RaiseEvent(new PowerSuppliesImportedEvent());
//            }

//            return true;
//        }
//    }
//}
