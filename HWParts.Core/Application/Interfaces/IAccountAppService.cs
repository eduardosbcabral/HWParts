using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task Register(RegisterAccountViewModel registerAccountViewModel);
        Task Login(LoginAccountViewModel loginAccountViewModel);
        Task ConfirmEmail(ConfirmEmailAccountViewModel confirmEmailAccountViewModel);

        PaginationObject<AccountViewModel> ListPaginated(int? page);
    }
}
