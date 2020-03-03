using HWParts.Core.Domain.ViewModels.Motherboard;
using HWParts.Core.Infrastructure.Common.Pagination;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Repositories
{
    public interface IMotherboardRepository
    {
        Task<PaginationObject<ListMotherboardViewModel>> PaginatedList(int pageNumber, int pageSize);
    }
}
