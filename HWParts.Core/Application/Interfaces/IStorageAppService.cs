using HWParts.Core.Application.ViewModels.Storage;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Interfaces
{
    public interface IStorageAppService
    {
        void Register(StorageViewModel storageViewModel);
        void Update(StorageViewModel storageViewModel);
        void Remove(Guid id);

        StorageViewModel GetById(Guid id);
        PaginationObject<StorageViewModel> ListPaginated(int? page);
    }
}
