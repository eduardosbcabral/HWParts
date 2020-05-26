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
    public class RegisterGraphicsCardCommandHandler : CommandHandler,
        IRequestHandler<RegisterGraphicsCardCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IGraphicsCardRepository _graphicsCardRepository;

        public RegisterGraphicsCardCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IGraphicsCardRepository graphicsCardRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _graphicsCardRepository = graphicsCardRepository;
        }

        public Task<bool> Handle(RegisterGraphicsCardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var graphicsCard = new GraphicsCard(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_graphicsCardRepository.GetByPlatformId(graphicsCard.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _graphicsCardRepository.Add(graphicsCard);

            if (Commit())
            {
                Bus.RaiseEvent(new GraphicsCardRegisteredEvent(graphicsCard.Id, graphicsCard.Brand, graphicsCard.Model));
            }

            return Task.FromResult(true);
        }
    }
}
