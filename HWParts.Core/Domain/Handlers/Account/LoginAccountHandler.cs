using AutoMapper;
using HWParts.Core.Application.Services;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Handlers.Responses.Accounts;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class LoginAccountHandler : IRequestHandler<LoginAccount, CommandResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public LoginAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResponse> Handle(LoginAccount request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.PasswordSignInAsync(
                request.UserName,
                request.Password,
                request.RememberMe);

            if (!result.Succeeded)
            {
                if (result.RequiresTwoFactor)
                {
                    return new ErrorCommandResponse("Ocorreu um erro. É necessário validar a conta utilizando a verificação de dois passos.");
                }

                if (result.IsLockedOut)
                {
                    return new ErrorCommandResponse("Ocorreu um erro. Sua conta está bloqueada.");
                }

                return new ErrorCommandResponse("Tentativa de login inválida.");
            }

            var account = await _accountRepository.FindByNameAsyncSafe(request.UserName);

            var token = TokenService.GenerateToken(account.UserName);

            return new SuccessCommandResponse(
                "Login realizado com sucesso.",
                new LoginAccountSuccess(token, account));
        }
    }
}

namespace HWParts.Core.Domain.Handlers.Responses.Accounts
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