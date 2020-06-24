using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.ComponentPrice;
using HWParts.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace HWParts.Core.Application.Services
{
    public class ComponentPriceAppService : IComponentPriceAppService
    {
        private readonly IComponentPriceRepository _repository;
        private readonly IMapper _mapper;

        public ComponentPriceAppService(
            IComponentPriceRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IList<ComponentPriceViewModel> GetAllPricesByComponentId(Guid componentId)
        {
            var prices = _repository.GetAllByComponentId(componentId);
            return _mapper.Map<IList<ComponentPriceViewModel>>(prices);
        }
    }
}
