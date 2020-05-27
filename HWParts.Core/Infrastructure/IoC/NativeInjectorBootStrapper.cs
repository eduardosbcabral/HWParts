using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.Services;
using HWParts.Core.Domain.CommandHandlers;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.EventHandlers;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Bus;
using HWParts.Core.Infrastructure.Repositories;
using HWParts.Core.Infrastructure.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HWParts.Core.Infrastructure.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>()

            // Application
            .AddScoped<IMotherboardAppService, MotherboardAppService>()
            .AddScoped<IGraphicsCardAppService, GraphicsCardAppService>()
            .AddScoped<IMemoryAppService, MemoryAppService>()

            // Domain - Events
            .AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>()
            .AddScoped<INotificationHandler<MotherboardRegisteredEvent>, MotherboardEventHandler>()
            .AddScoped<INotificationHandler<MotherboardUpdatedEvent>, MotherboardEventHandler>()

            // Domain - Commands
            .AddScoped<IRequestHandler<RegisterMotherboardCommand, bool>, MotherboardCommandHandler>()
            .AddScoped<IRequestHandler<UpdateMotherboardCommand, bool>, MotherboardCommandHandler>()
            .AddScoped<IRequestHandler<RemoveMotherboardCommand, bool>, MotherboardCommandHandler>()

            .AddScoped<IRequestHandler<RegisterGraphicsCardCommand, bool>, GraphicsCardCommandHandler>()
            .AddScoped<IRequestHandler<UpdateGraphicsCardCommand, bool>, GraphicsCardCommandHandler>()
            .AddScoped<IRequestHandler<RemoveGraphicsCardCommand, bool>, GraphicsCardCommandHandler>()

            .AddScoped<IRequestHandler<RegisterMemoryCommand, bool>, MemoryCommandHandler>()
            .AddScoped<IRequestHandler<UpdateMemoryCommand, bool>, MemoryCommandHandler>()
            .AddScoped<IRequestHandler<RemoveMemoryCommand, bool>, MemoryCommandHandler>()

            // Infra - Data
            .AddScoped<IMotherboardRepository, MotherboardRepository>()
            .AddScoped<IGraphicsCardRepository, GraphicsCardRepository>()
            .AddScoped<IMemoryRepository, MemoryRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<HWPartsDbContext>();

            // Infra - Data EventSourcing

            // Infra - Identity
        }
    }
}
