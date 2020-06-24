using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class ComponentPriceRepository : Repository<ComponentPrice>, IComponentPriceRepository
    {
        public ComponentPriceRepository(HWPartsDbContext context) 
            : base(context)
        {
        }

        public IList<ComponentPrice> GetAllByComponentId(Guid componentId)
        {
            return DbSet
                .Where(x => x.Component.Id == componentId)
                .Select(x => new ComponentPrice(
                    x.Id,
                    x.Price,
                    x.Platform)
                )
                .ToList();
        }
    }
}
