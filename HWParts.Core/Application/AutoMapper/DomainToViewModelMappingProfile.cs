using AutoMapper;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Entities;

namespace HWParts.Core.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Account, SafeAccountDTO>();
        }
    }
}
