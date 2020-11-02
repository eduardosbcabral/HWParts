using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Handlers.Responses;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class ForgotPasswordAccountHandler : IRequestHandler<ForgotPasswordAccount, CommandResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public ForgotPasswordAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResponse> Handle(ForgotPasswordAccount request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.FindByEmailAsync(request.Email);

            if (user == null || !(await _accountRepository.IsEmailConfirmedAsync(user)))
            {
                return new ErrorCommandResponse("Não foi possível resetar a senha.");
            }

            var code = await _accountRepository.GeneratePasswordResetTokenAsync(user);

            if (string.IsNullOrEmpty(code))
            {
                return new ErrorCommandResponse("Não foi possível resetar a senha.");
            }

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //await _emailSender.SendEmailAsync(request.Email, "Resetar Senha",
            //    $"Por favor, troque sua senha clicando aqui: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");

            return new SuccessCommandResponse(
                "Para trocar a sua senha acesse o link que foi enviado para o seu e-mail.",
                new ForgotPasswordAccountSuccess(code)
            );
        }
    }
}



namespace HWParts.Core.Domain.Handlers.Responses
{
    public class ForgotPasswordAccountSuccess
    {
        public string Token { get; private set; }

        public ForgotPasswordAccountSuccess(string token)
        {
            Token = token;
        }
    }
}