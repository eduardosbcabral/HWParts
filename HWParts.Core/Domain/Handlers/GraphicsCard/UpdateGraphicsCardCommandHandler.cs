using HWParts.Core.Domain.Bus;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Notifications;
using HWParts.Core.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class UpdateGraphicsCardCommandHandler : CommandHandler,
        IRequestHandler<UpdateGraphicsCardCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IGraphicsCardRepository _graphicsCardRepository;

        public UpdateGraphicsCardCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IGraphicsCardRepository graphicsCardRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _graphicsCardRepository = graphicsCardRepository;
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
    }
}
