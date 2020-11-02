using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IMediator _mediator;

        public AccountAppService(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResponse> Register(RegisterAccount command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CommandResponse> Login(LoginAccount command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CommandResponse> ConfirmEmail(ConfirmEmailAccount command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CommandResponse> ForgotPassword(ForgotPasswordAccount command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CommandResponse> ResetPassword(ResetPasswordAccount command)
        {
            return await _mediator.Send(command);
        }

        //#region Queries
        //public PaginationObject<AccountViewModel> ListPaginated(int? page)
        //{
        //    return _accountRepository.ListPaginated(page);
        //}
        //#endregion
    }
}
