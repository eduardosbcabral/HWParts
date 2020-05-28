using AutoMapper;
using HWParts.Core.Application.ViewModels.Storage;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Queries;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class StorageRepository : Repository<Storage>, IStorageRepository
    {
        private readonly IMapper _mapper;

        public StorageRepository(
            HWPartsDbContext context,
            IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public Storage GetByPlatformId(string platformId)
        {
            return DbSet
                .AsNoTracking()
                .FirstOrDefault(StorageQueries.GetByPlatformId(platformId));
        }

        public PaginationObject<StorageViewModel> ListPaginated(int? page)
        {
            return DbSet
                .AsNoTracking()
                .Pagination<Storage, StorageViewModel>(_mapper, page);
        }

        public bool Exists(Guid id)
        {
            return DbSet
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Any();
        }
    }
}
