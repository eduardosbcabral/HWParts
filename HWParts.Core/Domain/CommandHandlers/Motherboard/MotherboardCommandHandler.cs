using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class MotherboardCommandHandler : CommandHandler,
        IRequestHandler<RegisterMotherboardCommand, bool>,
        IRequestHandler<UpdateMotherboardCommand, bool>,
        IRequestHandler<RemoveMotherboardCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMotherboardRepository _motherboardRepository;

        public MotherboardCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IMotherboardRepository motherboardRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _motherboardRepository = motherboardRepository;
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
    }
}
