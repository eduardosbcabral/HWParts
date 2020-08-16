//using AutoMapper;
//using HWParts.Core.Application.Interfaces;
//using HWParts.Core.Domain.Commands;
//using HWParts.Core.Domain.Core.Bus;
//using HWParts.Core.Domain.Interfaces;
//using HWParts.Core.Infrastructure.Common.Pagination;
//using System;
//using System.Threading.Tasks;

//namespace HWParts.Core.Application.Services
//{
//    public class ProcessorAppService : IProcessorAppService
//    {
//        private readonly IMapper _mapper;
//        private readonly IMediatorHandler Bus;

//        private readonly IProcessorRepository _processorRepository;

//        public ProcessorAppService(
//            IMapper mapper,
//            IMediatorHandler bus,
//            IProcessorRepository processorRepository)
//        {
//            _mapper = mapper;
//            Bus = bus;

//            _processorRepository = processorRepository;
//        }

//        #region Commands
//        public void Register(ProcessorViewModel processorViewModel)
//        {
//            var registerCommand = _mapper.Map<RegisterProcessorCommand>(processorViewModel);
//            Bus.SendCommand(registerCommand);
//        }

//        public void Update(ProcessorViewModel processorViewModel)
//        {
//            var updateCommand = _mapper.Map<UpdateProcessorCommand>(processorViewModel);
//            Bus.SendCommand(updateCommand);
//        }

//        public void Remove(Guid id)
//        {
//            var removeCommand = new RemoveProcessorCommand(id);
//            Bus.SendCommand(removeCommand);
//        }

//        public Task Import(ImportProcessorsViewModel viewModel)
//        {
//            var command = _mapper.Map<ImportProcessorsCommand>(viewModel);
//            return Bus.SendCommand(command);
//        }
//        #endregion

//        #region Queries
//        public ProcessorViewModel GetById(Guid id)
//        {
//            var processor = _processorRepository.GetById(id);
//            return _mapper.Map<ProcessorViewModel>(processor);
//        }

//        public PaginationObject<ProcessorViewModel> ListPaginated(int? page)
//        {
//            return _processorRepository.ListPaginated(page);
//        }
//        #endregion
//    }
//}
