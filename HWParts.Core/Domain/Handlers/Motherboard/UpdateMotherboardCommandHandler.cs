using HWParts.Core.Domain.Bus;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Notifications;
using HWParts.Core.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class UpdateMotherboardCommandHandler : CommandHandler,
        IRequestHandler<UpdateMotherboardCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMotherboardRepository _motherboardRepository;

        public UpdateMotherboardCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IMotherboardRepository motherboardRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _motherboardRepository = motherboardRepository;
        }

        public Task<bool> Handle(UpdateMotherboardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if(!_motherboardRepository.Exists(request.Id))
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
    }
}

