using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Repositories;
using HWParts.Core.Domain.ViewModels.Admin.Motherboard;
using HWParts.Core.Infrastructure.Common;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class MotherboardRepository : RepositoryBase<Motherboard>, IMotherboardRepository
    {
        private readonly HWPartsDbContext _context;

        public MotherboardRepository(HWPartsDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<PaginationObject<ListMotherboardViewModelAdmin>> PaginatedList(int pageNumber, int pageSize)
        {
            var motherboardsQuery = await _context.Motherboards
                .OrderBy(x => x.Order)
                .Select(x => new ListMotherboardViewModelAdmin
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model
                })
                .AsNoTracking()
                .PaginationAsync(pageNumber, pageSize);

            return motherboardsQuery;
        }
    }
}
