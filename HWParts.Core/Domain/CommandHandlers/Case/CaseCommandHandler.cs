using HWParts.Core.Domain.CommandHandlers.Shared;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Events.Case;
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
    public class CaseCommandHandler : CommandHandler,
        IRequestHandler<RegisterCaseCommand, bool>,
        IRequestHandler<UpdateCaseCommand, bool>,
        IRequestHandler<RemoveCaseCommand, bool>,
        IRequestHandler<ImportCasesCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly ICaseRepository _caseRepository;

        private readonly HWPartsDbContext _context;

        public CaseCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            ICaseRepository caseRepository,
            HWPartsDbContext context)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _caseRepository = caseRepository;
            _context = context;
        }

        public Task<bool> Handle(RegisterCaseCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var caseEntity = new Case(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_caseRepository.GetByPlatformId(caseEntity.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _caseRepository.Add(caseEntity);

            if (Commit())
            {
                Bus.RaiseEvent(new CaseRegisteredEvent(caseEntity.Id, caseEntity.Brand, caseEntity.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCaseCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_caseRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var caseEntity = _caseRepository.GetByPlatformId(request.PlatformId);

            if (caseEntity != null && caseEntity.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            caseEntity.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _caseRepository.Update(caseEntity);

            if (Commit())
            {
                Bus.RaiseEvent(new CaseUpdatedEvent(caseEntity.Id, caseEntity.Brand, caseEntity.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveCaseCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _caseRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new CaseRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public async Task<bool> Handle(ImportCasesCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var cases = new List<Case>();

            using (var reader = new StreamReader(request.File.OpenReadStream()))
            {
                var json = await reader.ReadToEndAsync();

                var processorsDeserialized = JsonConvert.DeserializeObject<List<dynamic>>(json);

                foreach (var item in processorsDeserialized)
                {
                    var brand = ImportFileHelper.BindParameter<string>("Brand", item);
                    var model = ImportFileHelper.BindParameter<string>("Model", item);
                    var platformId = ImportFileHelper.BindParameter<string>("platform_id", item);
                    var imagesUrls = (ImportFileHelper.BindParameter<List<string>>("images_urls", item));
                    var url = ImportFileHelper.BindParameter<string>("url", item);
                    var platform = (EPlatform)Enum.Parse(typeof(EPlatform), ImportFileHelper.BindParameter<string>("platform", item), true);

                    var imageUrlString = string.Join(";", imagesUrls);

                    var caseEntity = new Case(
                        brand,
                        model,
                        platformId,
                        imageUrlString,
                        url,
                        platform);

                    var existsOnDb = _context.Cases
                            .Any(x => x.PlatformId == caseEntity.PlatformId);

                    if (!existsOnDb)
                    {
                        var existsOnCurrentList = cases.Any(x => x.PlatformId == caseEntity.PlatformId);

                        if (!existsOnCurrentList)
                        {
                            cases.Add(caseEntity);
                        }
                    }
                    else
                    {
                        try
                        {
                            var caseFromDb = _context.Cases
                                .SingleOrDefault(x => x.PlatformId == caseEntity.PlatformId);

                            caseFromDb.Update(platformId, imageUrlString, url, platform, brand, model);
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
                await Bus.RaiseEvent(new CasesImportedEvent());
            }

            return true;
        }
    }
}
