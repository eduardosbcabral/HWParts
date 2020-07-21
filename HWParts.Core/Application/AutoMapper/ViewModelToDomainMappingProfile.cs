using AutoMapper;
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
using System.Linq;

namespace HWParts.Core.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MotherboardViewModel, RegisterMotherboardCommand>()
                .ConvertUsing(c => new RegisterMotherboardCommand(c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<MotherboardViewModel, UpdateMotherboardCommand>()
                .ConvertUsing(c => new UpdateMotherboardCommand(c.Id, c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<ImportMotherboardsViewModel, ImportMotherboardsCommand>();

            CreateMap<GraphicsCardViewModel, RegisterGraphicsCardCommand>()
                .ConvertUsing(c => new RegisterGraphicsCardCommand(c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<GraphicsCardViewModel, UpdateGraphicsCardCommand>()
                .ConvertUsing(c => new UpdateGraphicsCardCommand(c.Id, c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<ImportGraphicsCardsViewModel, ImportGraphicsCardsCommand>();

            CreateMap<MemoryViewModel, RegisterMemoryCommand>()
                .ConvertUsing(c => new RegisterMemoryCommand(c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<MemoryViewModel, UpdateMemoryCommand>()
                .ConvertUsing(c => new UpdateMemoryCommand(c.Id, c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<ImportMemoriesViewModel, ImportMemoriesCommand>();

            CreateMap<ProcessorViewModel, RegisterProcessorCommand>()
                .ConvertUsing(c => new RegisterProcessorCommand(c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<ProcessorViewModel, UpdateProcessorCommand>()
                .ConvertUsing(c => new UpdateProcessorCommand(c.Id, c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<ImportProcessorsViewModel, ImportProcessorsCommand>();

            CreateMap<StorageViewModel, RegisterStorageCommand>()
                .ConvertUsing(c => new RegisterStorageCommand(c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<StorageViewModel, UpdateStorageCommand>()
                .ConvertUsing(c => new UpdateStorageCommand(c.Id, c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<ImportStoragesViewModel, ImportStoragesCommand>();

            CreateMap<CaseViewModel, RegisterCaseCommand>()
                .ConvertUsing(c => new RegisterCaseCommand(c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<CaseViewModel, UpdateCaseCommand>()
                .ConvertUsing(c => new UpdateCaseCommand(c.Id, c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<ImportCasesViewModel, ImportCasesCommand>();

            CreateMap<PowerSupplyViewModel, RegisterPowerSupplyCommand>()
                .ConvertUsing(c => new RegisterPowerSupplyCommand(c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<PowerSupplyViewModel, UpdatePowerSupplyCommand>()
                .ConvertUsing(c => new UpdatePowerSupplyCommand(c.Id, c.PlatformId, string.Join(";", c.ImagesUrls), c.Url, c.Platform.Value, c.Brand, c.Model));
            CreateMap<ImportPowerSuppliesViewModel, ImportPowerSuppliesCommand>();

            CreateMap<RegisterAccountViewModel, RegisterAccountCommand>();
            CreateMap<LoginAccountViewModel, LoginAccountCommand>();
            CreateMap<ConfirmEmailAccountViewModel, ConfirmEmailAccountCommand>();
            CreateMap<ForgotPasswordAccountViewModel, ForgotPasswordAccountCommand>();
            CreateMap<ResetPasswordAccountViewModel, ResetPasswordAccountCommand>();

            CreateMap<ComponentPriceViewModel, RegisterComponentPriceCommand>();
            CreateMap<ComponentPriceViewModel, UpdateComponentPriceCommand>();
        }
    }
}
