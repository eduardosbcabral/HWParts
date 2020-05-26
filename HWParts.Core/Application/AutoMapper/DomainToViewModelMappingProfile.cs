using AutoMapper;
using HWParts.Core.Application.ViewModels.GraphicsCard;
using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Domain.Entities;

namespace HWParts.Core.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Motherboard, MotherboardViewModel>();
            CreateMap<GraphicsCard, GraphicsCardViewModel>();
        }
    }
}
