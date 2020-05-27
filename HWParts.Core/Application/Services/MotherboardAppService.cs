using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Services
{
    public class MotherboardAppService : IMotherboardAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        private readonly IMotherboardRepository _motherboardRepository;

        public MotherboardAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IMotherboardRepository motherboardRepository)
        {
            _mapper = mapper;
            Bus = bus;

            _motherboardRepository = motherboardRepository;
        }

        #region Commands
        public void Register(MotherboardViewModel motherboardViewModel)
        {
            var registerCommand = _mapper.Map<RegisterMotherboardCommand>(motherboardViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(MotherboardViewModel motherboardViewModel)
        {
            var updateCommand = _mapper.Map<UpdateMotherboardCommand>(motherboardViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveMotherboardCommand(id);
            Bus.SendCommand(removeCommand);
        }
        #endregion

        #region Queries
        public MotherboardViewModel GetById(Guid id)
        {
            var motherboard = _motherboardRepository.GetById(id);
            return _mapper.Map<MotherboardViewModel>(motherboard);
        }

        public PaginationObject<MotherboardViewModel> ListPaginated(int? page)
        {
            return _motherboardRepository.ListPaginated(page);
        }
        #endregion
    }
}
