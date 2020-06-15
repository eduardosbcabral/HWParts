using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Case;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Services
{
    public class CaseAppService : ICaseAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        private readonly ICaseRepository _caseRepository;

        public CaseAppService(
            IMapper mapper,
            IMediatorHandler bus,
            ICaseRepository caseRepository)
        {
            _mapper = mapper;
            Bus = bus;

            _caseRepository = caseRepository;
        }

        #region Commands
        public void Register(CaseViewModel caseViewModel)
        {
            var registerCommand = _mapper.Map<RegisterCaseCommand>(caseViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(CaseViewModel caseViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCaseCommand>(caseViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCaseCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public Task ImportCases(ImportCasesViewModel viewModel)
        {
            var command = _mapper.Map<ImportCasesCommand>(viewModel);
            return Bus.SendCommand(command);
        }
        #endregion

        #region Queries
        public CaseViewModel GetById(Guid id)
        {
            var caseEntity = _caseRepository.GetById(id);
            return _mapper.Map<CaseViewModel>(caseEntity);
        }

        public PaginationObject<CaseViewModel> ListPaginated(int? page)
        {
            return _caseRepository.ListPaginated(page);
        }
        #endregion
    }
}
