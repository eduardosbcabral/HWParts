using HWParts.Core.Domain.Repositories;
using HWParts.Core.Domain.ViewModels.Processor;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class ProcessorRepository : IProcessorRepository
    {
        private readonly HWPartsDbContext _context;

        public ProcessorRepository(HWPartsDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationObject<ListProcessorViewModel>> PaginatedList(int pageNumber, int pageSize)
        {
            var processorsQuery = await _context.Processors
                .Select(x => new ListProcessorViewModel
                {
                    PlatformId = x.PlatformId,
                    Name = x.Name,
                    Brand = x.Brand,
                    Series = x.Series,
                    L3Cache = x.L3Cache,
                    L2Cache = x.L2Cache,
                    CoolingDevice = x.CoolingDevice,
                    ManufacturingTech = x.ManufacturingTech,
                    Model = x.Model,
                    Item = x.Item,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Url = x.Url,
                    Platform = x.Platform.ToString()
                })
                .AsNoTracking()
                .PaginationAsync(pageNumber, pageSize);

            return processorsQuery;
        }
    }
}