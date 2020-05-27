using AutoMapper;
using HWParts.Core.Application.ViewModels.Memory;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Queries;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class MemoryRepository : Repository<Memory>, IMemoryRepository
    {
        private readonly IMapper _mapper;

        public MemoryRepository(HWPartsDbContext context, IMapper mapper)
            : base(context)
        => _mapper = mapper;

        public Memory GetByPlatformId(string platformId)
        {
            return DbSet
                .AsNoTracking()
                .FirstOrDefault(MemoryQueries.GetByPlatformId(platformId));
        }

        public PaginationObject<MemoryViewModel> ListPaginated(int? page)
        {
            var MemorysQuery = DbSet
                .AsNoTracking()
                .Pagination<Memory, MemoryViewModel>(_mapper, page);

            return MemorysQuery;
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
