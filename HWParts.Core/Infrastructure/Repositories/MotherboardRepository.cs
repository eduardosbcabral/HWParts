using HWParts.Core.Domain.Repositories;
using HWParts.Core.Domain.ViewModels.Motherboard;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class MotherboardRepository : IMotherboardRepository
    {
        private readonly HWPartsDbContext _context;

        public MotherboardRepository(HWPartsDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationObject<ListMotherboardViewModel>> PaginatedList(int pageNumber, int pageSize)
        {
            var motherboardsQuery = await _context.Motherboards
                .Select(x => new ListMotherboardViewModel
                {
                    PlatformId = x.PlatformId,
                    Name = x.Name,
                    Brand = x.Brand,
                    Model = x.Model,
                    Item = x.Item,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Url = x.Url,
                    Platform = x.Platform.ToString()
                })
                .AsNoTracking()
                .PaginationAsync(pageNumber, pageSize);

            return motherboardsQuery;
        }
    }
}
