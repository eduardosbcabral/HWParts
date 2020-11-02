using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using MediatR;

namespace HWParts.Core.Application.Services
{
    public class CaseAppService : ICaseAppService
    {
        private readonly IMediator _mediator;

        public CaseAppService(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Commands
        public CommandResponse Register(RegisterCaseCommand command)
        {
            return _mediator.Send(command).Result;
        }
        #endregion

        //public void Update(CaseViewModel caseViewModel)
        //{
        //    var updateCommand = _mapper.Map<UpdateCaseCommand>(caseViewModel);
        //    Bus.SendCommand(updateCommand);
        //}

        //public void Remove(Guid id)
        //{
        //    var removeCommand = new RemoveCaseCommand(id);
        //    Bus.SendCommand(removeCommand);
        //}

        //public Task Import(ImportCasesViewModel viewModel)
        //{
        //    var command = _mapper.Map<ImportCasesCommand>(viewModel);
        //    return Bus.SendCommand(command);
        //}
        //#endregion

        //#region Queries
        //public CaseViewModel GetById(Guid id)
        //{
        //    var caseEntity = _caseRepository.GetById(id);
        //    return _mapper.Map<CaseViewModel>(caseEntity);
        //}

        //public PaginationObject<CaseViewModel> ListPaginated(int? page)
        //{
        //    return _caseRepository.ListPaginated(page);
        //}
        //
    }
}