using HWParts.Core.Domain.Bus;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Notifications;
using HWParts.Core.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class RemoveGraphicsCardCommandHandler : CommandHandler,
        IRequestHandler<RemoveGraphicsCardCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IGraphicsCardRepository _graphicsCardRepository;

        public RemoveGraphicsCardCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IGraphicsCardRepository graphicsCardRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _graphicsCardRepository = graphicsCardRepository;
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
