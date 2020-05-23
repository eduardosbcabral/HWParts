using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.ViewModels.Admin.Motherboard;
using HWParts.Core.Infrastructure.Common;
using HWParts.Core.Infrastructure.Common.Pagination;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Repositories
{
    public interface IMotherboardRepository : IRepositoryBase<Motherboard>
    {
        Task<PaginationObject<ListMotherboardViewModelAdmin>> PaginatedList(int pageNumber, int pageSize);
    }
}
