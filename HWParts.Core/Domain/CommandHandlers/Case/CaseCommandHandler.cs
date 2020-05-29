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
    public class CaseCommandHandler : CommandHandler,
        IRequestHandler<RegisterCaseCommand, bool>,
        IRequestHandler<UpdateCaseCommand, bool>,
        IRequestHandler<RemoveCaseCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly ICaseRepository _caseRepository;

        public CaseCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            ICaseRepository caseRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _caseRepository = caseRepository;
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
    }
}
