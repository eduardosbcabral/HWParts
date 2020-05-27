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
    public class ProcessorCommandHandler : CommandHandler,
        IRequestHandler<RegisterProcessorCommand, bool>,
        IRequestHandler<UpdateProcessorCommand, bool>,
        IRequestHandler<RemoveProcessorCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly IProcessorRepository _processorRepository;

        public ProcessorCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IProcessorRepository processorRepository)
            : base(uow, bus, notifications)
        {
            Bus = bus;
            _processorRepository = processorRepository;
        }

        public Task<bool> Handle(RegisterProcessorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var processor = new Processor(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_processorRepository.GetByPlatformId(processor.PlatformId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            _processorRepository.Add(processor);

            if (Commit())
            {
                Bus.RaiseEvent(new ProcessorRegisteredEvent(processor.Id, processor.Brand, processor.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateProcessorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (!_processorRepository.Exists(request.Id))
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente não existente."));
                return Task.FromResult(false);
            }

            var processor = _processorRepository.GetByPlatformId(request.PlatformId);

            if (processor != null && processor.Id != request.Id)
            {
                Bus.RaiseEvent(new DomainNotification(request.MessageType, "Componente com o ID da plaforma existente."));
                return Task.FromResult(false);
            }

            processor.Update(
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform,
                request.Brand,
                request.Model);

            _processorRepository.Update(processor);

            if (Commit())
            {
                Bus.RaiseEvent(new ProcessorUpdatedEvent(processor.Id, processor.Brand, processor.Model));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveProcessorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _processorRepository.Remove(request.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new ProcessorRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }
    }
}
