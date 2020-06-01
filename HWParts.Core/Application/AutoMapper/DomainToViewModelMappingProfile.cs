using AutoMapper;
using HWParts.Core.Application.ViewModels.Case;
using HWParts.Core.Application.ViewModels.GraphicsCard;
using HWParts.Core.Application.ViewModels.Memory;
using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Application.ViewModels.PowerSupply;
using HWParts.Core.Application.ViewModels.Processor;
using HWParts.Core.Application.ViewModels.Storage;
using HWParts.Core.Domain.Entities;
using System.Linq;

namespace HWParts.Core.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Motherboard, MotherboardViewModel>();
            CreateMap<GraphicsCard, GraphicsCardViewModel>();
            CreateMap<Memory, MemoryViewModel>();
            CreateMap<Processor, ProcessorViewModel>();
            CreateMap<Storage, StorageViewModel>();
            CreateMap<Case, CaseViewModel>()
                .AfterMap((opt, dest) => dest.ImagesUrls = opt.ImageUrl.Split(";"));
            CreateMap<PowerSupply, PowerSupplyViewModel>();
        }
    }
}
