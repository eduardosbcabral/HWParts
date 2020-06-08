using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Infrastructure.Common.Pagination;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IAccountRepository
    {
        PaginationObject<AccountViewModel> ListPaginated(int? page);
    }
}
