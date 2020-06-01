﻿using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.Services;
using HWParts.Core.Domain.CommandHandlers;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.EventHandlers;
using HWParts.Core.Domain.Events;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Bus;
using HWParts.Core.Infrastructure.Identity.Authorization;
using HWParts.Core.Infrastructure.Identity.Models;
using HWParts.Core.Infrastructure.Repositories;
using HWParts.Core.Infrastructure.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace HWParts.Core.Infrastructure.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>()

            // ASP.NET Authorization Polices
            .AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>()

            // Application
            .AddScoped<IMotherboardAppService, MotherboardAppService>()
            .AddScoped<IGraphicsCardAppService, GraphicsCardAppService>()
            .AddScoped<IMemoryAppService, MemoryAppService>()
            .AddScoped<IProcessorAppService, ProcessorAppService>()
            .AddScoped<IStorageAppService, StorageAppService>()
            .AddScoped<ICaseAppService, CaseAppService>()
            .AddScoped<IPowerSupplyAppService, PowerSupplyAppService>()

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

            .AddScoped<IRequestHandler<RegisterProcessorCommand, bool>, ProcessorCommandHandler>()
            .AddScoped<IRequestHandler<UpdateProcessorCommand, bool>, ProcessorCommandHandler>()
            .AddScoped<IRequestHandler<RemoveProcessorCommand, bool>, ProcessorCommandHandler>()

            .AddScoped<IRequestHandler<RegisterStorageCommand, bool>, StorageCommandHandler>()
            .AddScoped<IRequestHandler<UpdateStorageCommand, bool>, StorageCommandHandler>()
            .AddScoped<IRequestHandler<RemoveStorageCommand, bool>, StorageCommandHandler>()

            .AddScoped<IRequestHandler<RegisterCaseCommand, bool>, CaseCommandHandler>()
            .AddScoped<IRequestHandler<UpdateCaseCommand, bool>, CaseCommandHandler>()
            .AddScoped<IRequestHandler<RemoveCaseCommand, bool>, CaseCommandHandler>()

            // Infra - Data
            .AddScoped<IMotherboardRepository, MotherboardRepository>()
            .AddScoped<IGraphicsCardRepository, GraphicsCardRepository>()
            .AddScoped<IMemoryRepository, MemoryRepository>()
            .AddScoped<IProcessorRepository, ProcessorRepository>()
            .AddScoped<IStorageRepository, StorageRepository>()
            .AddScoped<ICaseRepository, CaseRepository>()
            .AddScoped<IPowerSupplyRepository, PowerSupplyRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<HWPartsDbContext>()

            // Infra - Data EventSourcing

            // Infra - Identity
            .AddScoped<IUser, AspNetUser>();
        }
    }
}
