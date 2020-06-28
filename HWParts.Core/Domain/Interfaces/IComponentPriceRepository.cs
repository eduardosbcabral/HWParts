using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IComponentPriceRepository : IRepository<ComponentPrice>
    {
        IList<ComponentPrice> GetAllByComponentId(Guid componentId);
        bool AlreadyRegisteredOnPlatform(Guid componentId, Guid componentPriceId, EPlatform platform);
    }
}
