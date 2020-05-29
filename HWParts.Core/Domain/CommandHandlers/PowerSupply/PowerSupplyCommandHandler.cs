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
    public class PowerSupplyCommandHandler : CommandHandler,
        IRequestHandler<RegisterPowerSupplyCommand, bool>,
        IRequestHandler<UpdatePowerSupplyCommand, bool>,
        IRequestHandler<RemovePowerSupplyCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IPowerSupplyRepository _powerSupplyRepository;

        public PowerSupplyCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IPowerSupplyRepository powerSupplyRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _powerSupplyRepository = powerSupplyRepository;
        }

        public Task<bool> Handle(RegisterPowerSupplyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var powerSupply = new PowerSupply(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_powerSupplyRepository.GetByPlatformId(powerSupply.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _powerSupplyRepository.Add(powerSupply);

            if (Commit())
            {
                Bus.RaiseEvent(new PowerSupplyRegisteredEvent(powerSupply.Id, powerSupply.Brand, powerSupply.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdatePowerSupplyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_powerSupplyRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var powerSupply = _powerSupplyRepository.GetByPlatformId(request.PlatformId);

            if (powerSupply != null && powerSupply.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            powerSupply.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _powerSupplyRepository.Update(powerSupply);

            if (Commit())
            {
                Bus.RaiseEvent(new PowerSupplyUpdatedEvent(powerSupply.Id, powerSupply.Brand, powerSupply.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemovePowerSupplyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _powerSupplyRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new PowerSupplyRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }
    }
}
