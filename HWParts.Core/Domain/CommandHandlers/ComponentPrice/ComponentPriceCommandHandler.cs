using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class ComponentPriceCommandHandler : CommandHandler,
        IRequestHandler<RegisterComponentPriceCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IComponentPriceRepository _componentPriceRepository;

        public ComponentPriceCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IComponentPriceRepository componentPriceRepository) 
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _componentPriceRepository = componentPriceRepository;
        }

        public Task<bool> Handle(RegisterComponentPriceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var component = new ComponentBase(request.ComponentBaseId);

            var componentPrice = new ComponentPrice(
                request.Price,
                request.Platform,
                component);

            return Task.FromResult(true);
        }
    }
}
