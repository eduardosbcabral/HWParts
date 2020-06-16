using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.PowerSupply;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Services
{
    public class PowerSupplyAppService : IPowerSupplyAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        private readonly IPowerSupplyRepository _powerSupplyRepository;

        public PowerSupplyAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IPowerSupplyRepository powerSupplyRepository)
        {
            _mapper = mapper;
            Bus = bus;

            _powerSupplyRepository = powerSupplyRepository;
        }

        #region Commands
        public void Register(PowerSupplyViewModel powerSupplyViewModel)
        {
            var registerCommand = _mapper.Map<RegisterPowerSupplyCommand>(powerSupplyViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(PowerSupplyViewModel powerSupplyViewModel)
        {
            var updateCommand = _mapper.Map<UpdatePowerSupplyCommand>(powerSupplyViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemovePowerSupplyCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public Task Import(ImportPowerSuppliesViewModel viewModel)
        {
            var command = _mapper.Map<ImportPowerSuppliesCommand>(viewModel);
            return Bus.SendCommand(command);
        }
        #endregion

        #region Queries
        public PowerSupplyViewModel GetById(Guid id)
        {
            var powerSupply = _powerSupplyRepository.GetById(id);
            return _mapper.Map<PowerSupplyViewModel>(powerSupply);
        }

        public PaginationObject<PowerSupplyViewModel> ListPaginated(int? page)
        {
            return _powerSupplyRepository.ListPaginated(page);
        }
        #endregion
    }
}
