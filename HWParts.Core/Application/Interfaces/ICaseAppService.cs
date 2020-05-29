using HWParts.Core.Application.ViewModels.Case;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Interfaces
{
    public interface ICaseAppService
    {
        void Register(CaseViewModel caseViewModel);
        void Update(CaseViewModel caseViewModel);
        void Remove(Guid id);

        CaseViewModel GetById(Guid id);
        PaginationObject<CaseViewModel> ListPaginated(int? page);
    }
}
