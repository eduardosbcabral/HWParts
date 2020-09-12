using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<CommandResponse> Register(RegisterAccount registerAccountViewModel);
        Task<CommandResponse> Login(LoginAccount command);
        //Task ConfirmEmail(ConfirmEmailAccountViewModel confirmEmailAccountViewModel);
        //Task ForgotPassword(ForgotPasswordAccountViewModel forgotPasswordAccountViewModel);
        //Task ResetPassword(ResetPasswordAccountViewModel resetPasswordAccountViewModel);

        //PaginationObject<AccountViewModel> ListPaginated(int? page);
    }
}
