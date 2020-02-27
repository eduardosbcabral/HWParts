using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Common.Pagination
{
    public static class PaginationExtension
    {
        public static async Task<PaginationObject<T>> PaginationAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var results = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var count = await query.CountAsync();

            var totalPages = (int)Math.Ceiling(decimal.Divide(count, pageSize));
            var firstPage = 1;
            var lastPage = totalPages;
            var prevPage = Math.Max(pageNumber - 1, firstPage);
            var nextPage = Math.Min(pageNumber + 1, lastPage);

            return new PaginationObject<T>(
                totalPages,
                firstPage,
                lastPage,
                prevPage,
                nextPage,
                results);
        }
    }
}
