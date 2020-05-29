using HWParts.Core.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace HWParts.Core.Domain.Queries
{
    public static class PowerSupplyQueries
    {
        public static Expression<Func<PowerSupply, bool>> GetByPlatformId(string platformId) =>
            x => x.PlatformId == platformId;
    }
}
