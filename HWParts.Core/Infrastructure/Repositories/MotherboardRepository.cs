using AutoMapper;
using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Queries;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class MotherboardRepository : Repository<Motherboard>, IMotherboardRepository
    {
        private readonly IMapper _mapper;

        public MotherboardRepository(HWPartsDbContext context, IMapper mapper)
            : base(context)
        => _mapper = mapper;

        public Motherboard GetByPlatformId(string platformId)
        {
            return DbSet
                .AsNoTracking()
                .FirstOrDefault(MotherboardQueries.GetByPlatformId(platformId));
        }

        public PaginationObject<MotherboardViewModel> ListPaginated(int? page)
        {
            var motherboardsQuery = DbSet
                .AsNoTracking()
                .Pagination<Motherboard, MotherboardViewModel>(_mapper, page);

            return motherboardsQuery;
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
