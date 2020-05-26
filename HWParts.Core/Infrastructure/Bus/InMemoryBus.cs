using HWParts.Core.Domain.Bus;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Events;
using MediatR;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        //public InMemoryBus(IEventStore eventStore, IMediator mediator)
        //{
        //    _eventStore = eventStore;
        //    _mediator = mediator;
        //}

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }


        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            //if (!@event.MessageType.Equals("DomainNotification"))
            //    _eventStore?.Save(@event);

            return _mediator.Publish(@event);
        }
    }
}
