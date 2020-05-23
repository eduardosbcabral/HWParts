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
                .Select(x => new ListProcessorViewModel())
                .AsNoTracking()
                .PaginationAsync(pageNumber, pageSize);

            return processorsQuery;
        }
    }
}