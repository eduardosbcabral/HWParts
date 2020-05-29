using HWParts.Core.Application.ViewModels.PowerSupply;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Interfaces
{
    public interface IPowerSupplyAppService
    {
        void Register(PowerSupplyViewModel powerSupplyViewModel);
        void Update(PowerSupplyViewModel powerSupplyViewModel);
        void Remove(Guid id);

        PowerSupplyViewModel GetById(Guid id);
        PaginationObject<PowerSupplyViewModel> ListPaginated(int? page);
    }
}
