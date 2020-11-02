using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class ResetPasswordAccountHandler : IRequestHandler<ResetPasswordAccount, CommandResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public ResetPasswordAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResponse> Handle(ResetPasswordAccount request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ErrorCommandResponse("Ocorreu um erro ao trocar a sua senha.");
            }

            var decodedCode = WebEncoders.Base64UrlDecode(request.Code);
            var code = Encoding.Default.GetString(decodedCode);

            var result = await _accountRepository.ResetPasswordAsync(user, code, request.Password);
            if (!result.Succeeded)
            {
                return new ErrorCommandResponse("Não foi possível trocar a sua senha.");
            }

            return new SuccessCommandResponse("Sua senha foi trocada com sucesso!");
        }
    }
}
