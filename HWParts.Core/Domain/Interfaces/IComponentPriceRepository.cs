using HWParts.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IComponentPriceRepository : IRepository<ComponentPrice>
    {
        IList<ComponentPrice> GetAllByComponentId(Guid componentId);
    }
}
