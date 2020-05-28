using HWParts.Core.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace HWParts.Core.Domain.Queries
{
    public static class StorageQueries
    {
        public static Expression<Func<Storage, bool>> GetByPlatformId(string platformId) =>
            x => x.PlatformId == platformId;
    }
}
