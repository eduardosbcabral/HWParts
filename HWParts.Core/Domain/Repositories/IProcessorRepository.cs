using HWParts.Core.Domain.ViewModels.Processor;
using HWParts.Core.Infrastructure.Common.Pagination;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Repositories
{
    public interface IProcessorRepository
    {
        Task<PaginationObject<ListProcessorViewModel>> PaginatedList(int pageNumber, int pageSize);
    }
}
