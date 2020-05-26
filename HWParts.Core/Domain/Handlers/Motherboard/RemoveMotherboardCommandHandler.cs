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
    public class RemoveMotherboardCommandHandler : CommandHandler,
        IRequestHandler<RemoveMotherboardCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IMotherboardRepository _motherboardRepository;

        public RemoveMotherboardCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IMotherboardRepository motherboardRepository) 
            : base(uow, bus, notifications)
        {
            Bus = bus;

            _motherboardRepository = motherboardRepository;
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
