using HWParts.Core.Application.ViewModels.PowerSupply;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IPowerSupplyRepository : IRepository<PowerSupply>
    {
        PowerSupply GetByPlatformId(string platformId);
        PaginationObject<PowerSupplyViewModel> ListPaginated(int? page);
        bool Exists(Guid id);
    }
}
