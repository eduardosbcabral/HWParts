using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Interfaces
{
    public interface IMotherboardAppService
    {
        void Register(MotherboardViewModel motherboardViewModel);
        void Update(MotherboardViewModel motherboardViewModel);
        void Remove(Guid id);

        MotherboardViewModel GetById(Guid id);
        PaginationObject<MotherboardViewModel> ListPaginated(int? page);
    }
}
