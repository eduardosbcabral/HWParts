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
    public class GraphicsCardCommandHandler : CommandHandler,
        IRequestHandler<RegisterGraphicsCardCommand, bool>,
        IRequestHandler<UpdateGraphicsCardCommand, bool>,
        IRequestHandler<RemoveGraphicsCardCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IGraphicsCardRepository _graphicsCardRepository;

        public GraphicsCardCommandHandler(
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

        public Task<bool> Handle(UpdateGraphicsCardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_graphicsCardRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var graphicsCard = _graphicsCardRepository.GetByPlatformId(request.PlatformId);

            if (graphicsCard != null && graphicsCard.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            graphicsCard.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _graphicsCardRepository.Update(graphicsCard);

            if (Commit())
            {
                Bus.RaiseEvent(new GraphicsCardUpdatedEvent(graphicsCard.Id, graphicsCard.Brand, graphicsCard.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveGraphicsCardCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _graphicsCardRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new GraphicsCardRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }
    }
}
