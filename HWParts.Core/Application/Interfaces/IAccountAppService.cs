using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Infrastructure.Common.Pagination;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task Register(RegisterAccountViewModel registerAccountViewModel);
        Task Login(LoginAccountViewModel loginAccountViewModel);
        Task ConfirmEmail(ConfirmEmailAccountViewModel confirmEmailAccountViewModel);
        Task ForgotPassword(ForgotPasswordAccountViewModel forgotPasswordAccountViewModel);
        Task ResetPassword(ResetPasswordAccountViewModel resetPasswordAccountViewModel);

        PaginationObject<AccountViewModel> ListPaginated(int? page);
    }
}
