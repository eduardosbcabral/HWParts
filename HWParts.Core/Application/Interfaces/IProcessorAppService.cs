using HWParts.Core.Application.ViewModels.Processor;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Interfaces
{
    public interface IProcessorAppService
    {
        void Register(ProcessorViewModel processorViewModel);
        void Update(ProcessorViewModel processorViewModel);
        void Remove(Guid id);

        ProcessorViewModel GetById(Guid id);
        PaginationObject<ProcessorViewModel> ListPaginated(int? page);
    }
}
