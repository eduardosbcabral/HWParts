﻿using AutoMapper;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Application.ViewModels.Case;
using HWParts.Core.Application.ViewModels.ComponentPrice;
using HWParts.Core.Application.ViewModels.GraphicsCard;
using HWParts.Core.Application.ViewModels.Memory;
using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Application.ViewModels.PowerSupply;
using HWParts.Core.Application.ViewModels.Processor;
using HWParts.Core.Application.ViewModels.Storage;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MotherboardViewModel, RegisterMotherboardCommand>()
                .ConstructUsing(c => new RegisterMotherboardCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<MotherboardViewModel, UpdateMotherboardCommand>()
                .ConstructUsing(c => new UpdateMotherboardCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<ImportMotherboardsViewModel, ImportMotherboardsCommand>();

            CreateMap<GraphicsCardViewModel, RegisterGraphicsCardCommand>()
                .ConstructUsing(c => new RegisterGraphicsCardCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<GraphicsCardViewModel, UpdateGraphicsCardCommand>()
                .ConstructUsing(c => new UpdateGraphicsCardCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<ImportGraphicsCardsViewModel, ImportGraphicsCardsCommand>();

            CreateMap<MemoryViewModel, RegisterMemoryCommand>()
                .ConstructUsing(c => new RegisterMemoryCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<MemoryViewModel, UpdateMemoryCommand>()
                .ConstructUsing(c => new UpdateMemoryCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<ImportMemoriesViewModel, ImportMemoriesCommand>();

            CreateMap<ProcessorViewModel, RegisterProcessorCommand>()
                .ConstructUsing(c => new RegisterProcessorCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<ProcessorViewModel, UpdateProcessorCommand>()
                .ConstructUsing(c => new UpdateProcessorCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<ImportProcessorsViewModel, ImportProcessorsCommand>();

            CreateMap<StorageViewModel, RegisterStorageCommand>()
                .ConstructUsing(c => new RegisterStorageCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<StorageViewModel, UpdateStorageCommand>()
                .ConstructUsing(c => new UpdateStorageCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<ImportStoragesViewModel, ImportStoragesCommand>();

            CreateMap<CaseViewModel, RegisterCaseCommand>()
                .ConstructUsing(c => new RegisterCaseCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<CaseViewModel, UpdateCaseCommand>()
                .ConstructUsing(c => new UpdateCaseCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<ImportCasesViewModel, ImportCasesCommand>();

            CreateMap<PowerSupplyViewModel, RegisterPowerSupplyCommand>()
                .ConstructUsing(c => new RegisterPowerSupplyCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<PowerSupplyViewModel, UpdatePowerSupplyCommand>()
                .ConstructUsing(c => new UpdatePowerSupplyCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
            CreateMap<ImportPowerSuppliesViewModel, ImportPowerSuppliesCommand>();

            CreateMap<RegisterAccountViewModel, RegisterAccountCommand>();
            CreateMap<LoginAccountViewModel, LoginAccountCommand>();
            CreateMap<ConfirmEmailAccountViewModel, ConfirmEmailAccountCommand>();
            CreateMap<ForgotPasswordAccountViewModel, ForgotPasswordAccountCommand>();
            CreateMap<ResetPasswordAccountViewModel, ResetPasswordAccountCommand>();

            CreateMap<ComponentPriceViewModel, RegisterComponentPriceCommand>();
        }
    }
}
