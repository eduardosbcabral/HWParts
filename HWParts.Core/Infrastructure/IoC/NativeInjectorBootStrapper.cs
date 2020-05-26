using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.Services;
using HWParts.Core.Domain.Bus;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.EventHandlers;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Handlers;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Notifications;
using HWParts.Core.Domain.Repositories;
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
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IMotherboardAppService, MotherboardAppService>();
            services.AddScoped<IGraphicsCardAppService, GraphicsCardAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<MotherboardRegisteredEvent>, MotherboardEventHandler>();
            services.AddScoped<INotificationHandler<MotherboardUpdatedEvent>, MotherboardEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterMotherboardCommand, bool>, RegisterMotherboardCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateMotherboardCommand, bool>, UpdateMotherboardCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveMotherboardCommand, bool>, RemoveMotherboardCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterGraphicsCardCommand, bool>, RegisterGraphicsCardCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateGraphicsCardCommand, bool>, UpdateGraphicsCardCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveGraphicsCardCommand, bool>, RemoveGraphicsCardCommandHandler>();

            // Infra - Data
            services.AddScoped<IMotherboardRepository, MotherboardRepository>();
            services.AddScoped<IGraphicsCardRepository, GraphicsCardRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<HWPartsDbContext>();

            // Infra - Data EventSourcing

            // Infra - Identity
        }
    }
}
