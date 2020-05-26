using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Domain.Repositories
{
    public interface IMotherboardRepository : IRepository<Motherboard>
    {
        Motherboard GetByPlatformId(string platformId);
        PaginationObject<MotherboardViewModel> ListPaginated(int? page);
        bool Exists(Guid id);
    }
}
