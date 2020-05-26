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
    public class RegisterMotherboardCommandHandler : CommandHandler,
        IRequestHandler<RegisterMotherboardCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMotherboardRepository _motherboardRepository;

        public RegisterMotherboardCommandHandler(
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
            if(!request.IsValid())
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

            if(_motherboardRepository.GetByPlatformId(motherboard.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _motherboardRepository.Add(motherboard);

            if(Commit())
            {
                Bus.RaiseEvent(new MotherboardRegisteredEvent(motherboard.Id, motherboard.Brand, motherboard.Model));
            }

            return Task.FromResult(true);
        }
    }
}
