using AutoMapper;
using HWParts.Core.Application.Services;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.CommandHandlers.Responses.Accounts;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, CommandResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public LoginAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.PasswordSignInAsync(
                request.Username,
                request.Password,
                request.RememberMe);

            if (!result.Succeeded)
            {
                return new ErrorCommandResponse("Login", "Tentativa de login inválida.");
            }

            if (result.RequiresTwoFactor)
            {
                return new ErrorCommandResponse("RequiresTwoFactor", string.Empty);
            }

            if (result.IsLockedOut)
            {
                return new ErrorCommandResponse("IsLockedOut", string.Empty);
            }

            var account = await _accountRepository.FindByNameAsyncSafe(request.Username);

            var token = TokenService.GenerateToken(account.UserName);

            return new SuccessCommandResponse(new LoginAccountSuccess(token, account));
        }
    }
}

namespace HWParts.Core.Domain.CommandHandlers.Responses.Accounts
{
    public class LoginAccountSuccess
    {
        public SafeAccountDTO Account { get; private set; }
        public string Token { get; private set; }

        public LoginAccountSuccess(string token, SafeAccountDTO account)
        {
            Token = token;
            Account = account;
        }
    }
}