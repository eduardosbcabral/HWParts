using HWParts.Core.Application.ViewModels.Memory;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Domain.Repositories
{
    public interface IMemoryRepository : IRepository<Memory>
    {
        Memory GetByPlatformId(string platformId);
        PaginationObject<MemoryViewModel> ListPaginated(int? page);
        bool Exists(Guid id);
    }
}
