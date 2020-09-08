using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<CommandResponse> Register(RegisterAccountCommand registerAccountViewModel);
        //void Login(LoginAccountCommand command);
        //Task ConfirmEmail(ConfirmEmailAccountViewModel confirmEmailAccountViewModel);
        //Task ForgotPassword(ForgotPasswordAccountViewModel forgotPasswordAccountViewModel);
        //Task ResetPassword(ResetPasswordAccountViewModel resetPasswordAccountViewModel);

        //PaginationObject<AccountViewModel> ListPaginated(int? page);
    }
}
