using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<CommandResponse> Register(RegisterAccount command);
        Task<CommandResponse> Login(LoginAccount command);
        Task<CommandResponse> ConfirmEmail(ConfirmEmailAccount command);
        Task<CommandResponse> ForgotPassword(ForgotPasswordAccount command);
        Task<CommandResponse> ResetPassword(ResetPasswordAccount command);

        //PaginationObject<AccountViewModel> ListPaginated(int? page);
    }
}
