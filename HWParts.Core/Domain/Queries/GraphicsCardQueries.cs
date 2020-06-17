using HWParts.Core.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace HWParts.Core.Domain.Queries
{
    public static class GraphicsCardQueries
    {
        public static Expression<Func<GraphicsCard, bool>> GetByPlatformId(string platformId) =>
            x => x.ComponentBase.PlatformId == platformId;
    }
}
