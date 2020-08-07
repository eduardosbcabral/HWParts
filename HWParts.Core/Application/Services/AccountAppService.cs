using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        private readonly IAccountRepository _accountRepository;

        public AccountAppService(
            IMapper mapper,
            IMediatorHandler mediatorHandler,
            IAccountRepository accountRepository)
        {
            _mapper = mapper;
            Bus = mediatorHandler;
            _accountRepository = accountRepository;
        }

        public async Task Register(RegisterAccountCommand command)
        {
            await Bus.SendCommand(command);
        }

        public async void Login(LoginAccountCommand command)
        {
            await Bus.SendCommand(command);
        }

        public Task ConfirmEmail(ConfirmEmailAccountViewModel confirmEmailAccountViewModel)
        {
            var command = _mapper.Map<ConfirmEmailAccountCommand>(confirmEmailAccountViewModel);
            return Bus.SendCommand(command);
        }

        public Task ForgotPassword(ForgotPasswordAccountViewModel forgotPasswordAccountViewModel)
        {
            var command = _mapper.Map<ForgotPasswordAccountCommand>(forgotPasswordAccountViewModel);
            return Bus.SendCommand(command);
        }

        public Task ResetPassword(ResetPasswordAccountViewModel resetPasswordAccountViewModel)
        {
            var command = _mapper.Map<ResetPasswordAccountCommand>(resetPasswordAccountViewModel);
            return Bus.SendCommand(command);
        }

        #region Queries
        public PaginationObject<AccountViewModel> ListPaginated(int? page)
        {
            return _accountRepository.ListPaginated(page);
        }
        #endregion
    }
}
