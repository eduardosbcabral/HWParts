using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Handlers.Responses;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class RegisterAccountHandler : IRequestHandler<RegisterAccount, CommandResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public RegisterAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResponse> Handle(RegisterAccount command, CancellationToken cancellationToken)
        {
            var user = new Account(
                command.UserName,
                command.Email);

            var result = await _accountRepository.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                var response = new ErrorCommandResponse("Erro ao cadastrar o usuário.");
                foreach (var error in result.Errors)
                {
                    response.AddNotification(null, error.Description);
                }

                return response;
            }

            var addToRoleResult = await _accountRepository.AddToRoleAsync(user, "Common");
            if (!addToRoleResult.Succeeded)
            {
                var response = new ErrorCommandResponse("Erro ao adicionar o cargo ao usuário.");
                foreach (var error in addToRoleResult.Errors)
                {
                    response.AddNotification(null, error.Description);
                }

                return response;
            }

            // TOKEN RETURN FOR DEVELOPMENT PURPOSES (REMOVE THIS IN PRODUCTION)
            string token = "Está configurado para não confirmar conta após registro.";

            if (_accountRepository.UserManager.Options.SignIn.RequireConfirmedAccount)
            {
                var code = await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                token = code;
            }

            return new SuccessCommandResponse(
                "Usuário cadastrado com sucesso.",
                new RegisterAccountSuccess(user.Id, token));
        }
    }
}

namespace HWParts.Core.Domain.Handlers.Responses
{
    public class RegisterAccountSuccess
    {
        public string Id { get; private set; }

        // TOKEN RETURN FOR DEVELOPMENT PURPOSES (REMOVE THIS IN PRODUCTION)
        public string TokenEmail { get; private set; }

        public RegisterAccountSuccess(string id, string token)
        {
            Id = id;
            TokenEmail = token;
        }
    }
}