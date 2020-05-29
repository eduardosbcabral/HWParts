using AutoMapper;
using HWParts.Core.Application.ViewModels.Case;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Queries;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        private readonly IMapper _mapper;

        public CaseRepository(
            HWPartsDbContext context,
            IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public Case GetByPlatformId(string platformId)
        {
            return DbSet
                .AsNoTracking()
                .FirstOrDefault(CaseQueries.GetByPlatformId(platformId));
        }

        public PaginationObject<CaseViewModel> ListPaginated(int? page)
        {
            return DbSet
                .AsNoTracking()
                .Pagination<Case, CaseViewModel>(_mapper, page);
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
