using HWParts.Core.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace HWParts.Core.Domain.Queries
{
    public static class CaseQueries
    {
        public static Expression<Func<Case, bool>> GetByPlatformId(string platformId) =>
            x => x.PlatformId == platformId;
    }
}
