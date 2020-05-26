using HWParts.Core.Application.ViewModels.Memory;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Interfaces
{
    public interface IMemoryAppService
    {
        void Register(MemoryViewModel memoryViewModel);
        void Update(MemoryViewModel memoryViewModel);
        void Remove(Guid id);

        MemoryViewModel GetById(Guid id);
        PaginationObject<MemoryViewModel> ListPaginated(int? page);
    }
}
