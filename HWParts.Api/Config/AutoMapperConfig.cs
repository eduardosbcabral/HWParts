using AutoMapper;
using HWParts.Core.Domain.Commands.Admin.Motherboards;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.ViewModels;
using HWParts.Core.Domain.ViewModels.Admin.Motherboard;
using HWParts.Core.Domain.ViewModels.Shared;

namespace HWParts.Api.Config
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EntityBase, EntityBaseViewModel>();
                cfg.CreateMap<ComponentBase, ComponentBaseViewModel>();
                cfg.CreateMap<Motherboard, EditMotherboardViewModel>();
                cfg.CreateMap<EditMotherboardViewModel, EditMotherboardCommand>();
                cfg.CreateMap<CreateMotherboardViewModel, CreateMotherboardCommand>();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
