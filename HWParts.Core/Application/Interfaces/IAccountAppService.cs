using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Infrastructure.Common.Pagination;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task Register(RegisterAccountViewModel registerAccountViewModel);
        Task Login(LoginAccountViewModel loginAccountViewModel);

        PaginationObject<AccountViewModel> ListPaginated(int? page);
    }
}
