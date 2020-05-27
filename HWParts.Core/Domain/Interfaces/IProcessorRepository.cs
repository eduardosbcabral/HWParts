using HWParts.Core.Application.ViewModels.Processor;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IProcessorRepository : IRepository<Processor>
    {
        Processor GetByPlatformId(string platformId);
        PaginationObject<ProcessorViewModel> ListPaginated(int? page);
        bool Exists(Guid id);
    }
}
