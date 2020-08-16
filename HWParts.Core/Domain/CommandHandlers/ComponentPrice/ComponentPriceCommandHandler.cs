//using HWParts.Core.Domain.Commands;
//using HWParts.Core.Domain.Core.Bus;
//using HWParts.Core.Domain.Core.Notifications;
//using HWParts.Core.Domain.Entities;
//using HWParts.Core.Domain.Events;
//using HWParts.Core.Domain.Interfaces;
//using MediatR;
//using System.ComponentModel;
//using System.Threading;
//using System.Threading.Tasks;

//namespace HWParts.Core.Domain.CommandHandlers
//{
//    public class ComponentPriceCommandHandler : CommandHandler,
//        IRequestHandler<RegisterComponentPriceCommand, bool>,
//        IRequestHandler<UpdateComponentPriceCommand, bool>,
//        IRequestHandler<RemoveComponentPriceCommand, bool>
//    {
//        private readonly IMediatorHandler Bus;
//        private readonly IComponentPriceRepository _componentPriceRepository;
//        private readonly IComponentBaseRepository _componentBaseRepository;

//        public ComponentPriceCommandHandler(
//            IUnitOfWork uow,
//            IMediatorHandler bus,
//            INotificationHandler<DomainNotification> notifications,
//            IComponentPriceRepository componentPriceRepository,
//            IComponentBaseRepository componentBaseRepository) 
//            : base(uow, bus, notifications)
//        {
//            Bus = bus;
//            _componentPriceRepository = componentPriceRepository;
//            _componentBaseRepository = componentBaseRepository;
//        }

//        public Task<bool> Handle(RegisterComponentPriceCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            var component = _componentBaseRepository.GetById(request.ComponentBaseId);

//            var componentPrice = new ComponentPrice(
//                request.Price,
//                request.Url,
//                request.Platform,
//                component);

//            if (_componentPriceRepository.AlreadyRegisteredOnPlatform(component.Id, componentPrice.Id, componentPrice.Platform))
//            {
//                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Preço do componente já foi registrado na plataforma."));
//                return Task.FromResult(false);
//            }

//            _componentPriceRepository.Add(componentPrice);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new ComponentPriceRegisteredEvent());
//            }

//            return Task.FromResult(true);
//        }

//        public Task<bool> Handle(UpdateComponentPriceCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            var componentPrice = _componentPriceRepository.GetById(request.Id);

//            componentPrice.Update(
//                request.Price,
//                request.Url,
//                request.Platform);

//            if (_componentPriceRepository.AlreadyRegisteredOnPlatform(componentPrice.Component.Id, componentPrice.Id, componentPrice.Platform))
//            {
//                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Preço do componente já foi registrado na plataforma."));
//                return Task.FromResult(false);
//            }

//            _componentPriceRepository.Update(componentPrice);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new ComponentPriceUpdatedEvent());
//            }

//            return Task.FromResult(true);
//        }

//        public Task<bool> Handle(RemoveComponentPriceCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return Task.FromResult(false);
//            }

//            _componentPriceRepository.Remove(request.Id);

//            if (Commit())
//            {
//                Bus.RaiseEvent(new ComponentPriceRemovedEvent(request.Id));
//            }

//            return Task.FromResult(true);
//        }
//    }
//}
