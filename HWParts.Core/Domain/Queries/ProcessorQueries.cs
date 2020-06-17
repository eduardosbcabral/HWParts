using HWParts.Core.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace HWParts.Core.Domain.Queries
{
    public static class ProcessorQueries
    {
        public static Expression<Func<Processor, bool>> GetByPlatformId(string platformId) =>
            x => x.ComponentBase.PlatformId == platformId;
    }
}
