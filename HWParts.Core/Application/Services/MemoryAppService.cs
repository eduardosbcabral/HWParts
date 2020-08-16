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
//    public class MemoryAppService : IMemoryAppService
//    {
//        private readonly IMapper _mapper;
//        private readonly IMediatorHandler Bus;

//        private readonly IMemoryRepository _memoryRepository;

//        public MemoryAppService(
//            IMapper mapper,
//            IMediatorHandler bus,
//            IMemoryRepository memoryRepository)
//        {
//            _mapper = mapper;
//            Bus = bus;

//            _memoryRepository = memoryRepository;
//        }

//        #region Commands
//        public void Register(MemoryViewModel memoryViewModel)
//        {
//            var registerCommand = _mapper.Map<RegisterMemoryCommand>(memoryViewModel);
//            Bus.SendCommand(registerCommand);
//        }

//        public void Update(MemoryViewModel memoryViewModel)
//        {
//            var updateCommand = _mapper.Map<UpdateMemoryCommand>(memoryViewModel);
//            Bus.SendCommand(updateCommand);
//        }

//        public void Remove(Guid id)
//        {
//            var removeCommand = new RemoveMemoryCommand(id);
//            Bus.SendCommand(removeCommand);
//        }

//        public Task Import(ImportMemoriesViewModel viewModel)
//        {
//            var command = _mapper.Map<ImportMemoriesCommand>(viewModel);
//            return Bus.SendCommand(command);
//        }
//        #endregion

//        #region Queries
//        public MemoryViewModel GetById(Guid id)
//        {
//            var memory = _memoryRepository.GetById(id);
//            return _mapper.Map<MemoryViewModel>(memory);
//        }

//        public PaginationObject<MemoryViewModel> ListPaginated(int? page)
//        {
//            return _memoryRepository.ListPaginated(page);
//        }
//        #endregion
//    }
//}
