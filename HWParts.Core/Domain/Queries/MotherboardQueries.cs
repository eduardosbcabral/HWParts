using HWParts.Core.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace HWParts.Core.Domain.Queries
{
    public static class MotherboardQueries
    {
        public static Expression<Func<Motherboard, bool>> GetByPlatformId(string platformId) =>
            x => x.PlatformId == platformId;
    }
}
