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
    public class ConfirmEmailAccountHandler : IRequestHandler<ConfirmEmailAccount, CommandResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public ConfirmEmailAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResponse> Handle(ConfirmEmailAccount request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.FindByIdAsync(request.Id);
            
            if (user == null)
            {
                return new ErrorCommandResponse("Ocorreu um erro ao encontrar o usuário.");
            }

            var decodedCode = WebEncoders.Base64UrlDecode(request.Code);
            var code = Encoding.Default.GetString(decodedCode);
            var result = await _accountRepository.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                return new ErrorCommandResponse("Ocorreu um erro ao confirmar o e-mail.");
            }

            return new SuccessCommandResponse("E-mail confirmado com sucesso.");
        }
    }
}
