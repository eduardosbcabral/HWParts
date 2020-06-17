using HWParts.Core.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace HWParts.Core.Domain.Queries
{
    public static class MemoryQueries
    {
        public static Expression<Func<Memory, bool>> GetByPlatformId(string platformId) =>
            x => x.ComponentBase.PlatformId == platformId;
    }
}
