using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using HWParts.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class ComponentPriceRepository : Repository<ComponentPrice>, IComponentPriceRepository
    {
        public ComponentPriceRepository(HWPartsDbContext context) 
            : base(context)
        {
        }

        public override ComponentPrice GetById(Guid id)
        {
            return DbSet
                .Include(x => x.Component)
                .SingleOrDefault(x => x.Id == id);
        }

        public IList<ComponentPrice> GetAllByComponentId(Guid componentId)
        {
            return DbSet
                .Where(x => x.Component.Id == componentId)
                .Select(x => new ComponentPrice(
                    x.Id,
                    x.Price,
                    x.Url,
                    x.Platform)
                )
                .ToList();
        }

        public bool AlreadyRegisteredOnPlatform(Guid componentId, Guid componentPriceId, EPlatform platform)
        {
            return DbSet
                .Where(x => x.Component.Id == componentId)
                .Where(x => x.Id != componentPriceId)
                .Where(x => x.Platform == platform)
                .Any();
        }
    }
}
