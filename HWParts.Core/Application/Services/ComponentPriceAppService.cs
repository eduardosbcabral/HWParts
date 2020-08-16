//using AutoMapper;
//using HWParts.Core.Application.Interfaces;
//using HWParts.Core.Domain.Commands;
//using HWParts.Core.Domain.Core.Bus;
//using HWParts.Core.Domain.Interfaces;
//using System;
//using System.Collections.Generic;

//namespace HWParts.Core.Application.Services
//{
//    public class ComponentPriceAppService : IComponentPriceAppService
//    {
//        private readonly IComponentPriceRepository _repository;
//        private readonly IMapper _mapper;
//        private readonly IMediatorHandler Bus;

//        public ComponentPriceAppService(
//            IComponentPriceRepository repository,
//            IMapper mapper,
//            IMediatorHandler bus)
//        {
//            _repository = repository;
//            _mapper = mapper;
//            Bus = bus;
//        }

//        public IList<ComponentPriceViewModel> GetAllPricesByComponentId(Guid componentId)
//        {
//            var prices = _repository.GetAllByComponentId(componentId);
//            return _mapper.Map<IList<ComponentPriceViewModel>>(prices);
//        }

//        public ComponentPriceViewModel GetById(Guid componentPriceId)
//        {
//            var price = _repository.GetById(componentPriceId);
//            return _mapper.Map<ComponentPriceViewModel>(price);
//        }

//        public void Register(ComponentPriceViewModel viewModel)
//        {
//            var command = _mapper.Map<RegisterComponentPriceCommand>(viewModel);
//            Bus.SendCommand(command);
//        }

//        public void Update(ComponentPriceViewModel viewModel)
//        {
//            var command = _mapper.Map<UpdateComponentPriceCommand>(viewModel);
//            Bus.SendCommand(command);
//        }

//        public void Remove(Guid id)
//        {
//            var removeCommand = new RemoveComponentPriceCommand(id);
//            Bus.SendCommand(removeCommand);
//        }
//    }
//}
