using AutoMapper;
using HWParts.Core.Application.ViewModels.Processor;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Domain.Queries;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class ProcessorRepository : Repository<Processor>, IProcessorRepository
    {
        private readonly IMapper _mapper;

        public ProcessorRepository(
            HWPartsDbContext context,
            IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public Processor GetByPlatformId(string platformId)
        {
            return DbSet
                .AsNoTracking()
                .FirstOrDefault(ProcessorQueries.GetByPlatformId(platformId));
        }

        public PaginationObject<ProcessorViewModel> ListPaginated(int? page)
        {
            return DbSet
                .AsNoTracking()
                .Pagination<Processor, ProcessorViewModel>(_mapper, page);
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
