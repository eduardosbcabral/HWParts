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
//    public class StorageAppService : IStorageAppService
//    {
//        private readonly IMapper _mapper;
//        private readonly IMediatorHandler Bus;

//        private readonly IStorageRepository _storageRepository;

//        public StorageAppService(
//            IMapper mapper,
//            IMediatorHandler bus,
//            IStorageRepository storageRepository)
//        {
//            _mapper = mapper;
//            Bus = bus;

//            _storageRepository = storageRepository;
//        }

//        #region Commands
//        public void Register(StorageViewModel storageViewModel)
//        {
//            var registerCommand = _mapper.Map<RegisterStorageCommand>(storageViewModel);
//            Bus.SendCommand(registerCommand);
//        }

//        public void Update(StorageViewModel storageViewModel)
//        {
//            var updateCommand = _mapper.Map<UpdateStorageCommand>(storageViewModel);
//            Bus.SendCommand(updateCommand);
//        }

//        public void Remove(Guid id)
//        {
//            var removeCommand = new RemoveStorageCommand(id);
//            Bus.SendCommand(removeCommand);
//        }

//        public Task Import(ImportStoragesViewModel viewModel)
//        {
//            var command = _mapper.Map<ImportStoragesCommand>(viewModel);
//            return Bus.SendCommand(command);
//        }
//        #endregion

//        #region Queries
//        public StorageViewModel GetById(Guid id)
//        {
//            var storage = _storageRepository.GetById(id);
//            return _mapper.Map<StorageViewModel>(storage);
//        }

//        public PaginationObject<StorageViewModel> ListPaginated(int? page)
//        {
//            return _storageRepository.ListPaginated(page);
//        }
//        #endregion
//    }
//}
