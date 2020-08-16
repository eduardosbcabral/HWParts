//using AutoMapper;
//using HWParts.Core.Application.ViewModels.GraphicsCard;
//using HWParts.Core.Domain.Entities;
//using HWParts.Core.Domain.Interfaces;
//using HWParts.Core.Domain.Queries;
//using HWParts.Core.Infrastructure.Common.Pagination;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;

//namespace HWParts.Core.Infrastructure.Repositories
//{
//    public class GraphicsCardRepository : Repository<GraphicsCard>, IGraphicsCardRepository
//    {
//        private readonly IMapper _mapper;

//        public GraphicsCardRepository(
//            HWPartsDbContext context,
//            IMapper mapper) 
//            : base(context)
//        {
//            _mapper = mapper;
//        }

//        public GraphicsCard GetByPlatformId(string platformId)
//        {
//            return DbSet
//                .AsNoTracking()
//                .FirstOrDefault(GraphicsCardQueries.GetByPlatformId(platformId));
//        }

//        public PaginationObject<GraphicsCardViewModel> ListPaginated(int? page)
//        {
//            var motherboardsQuery = DbSet
//                .AsNoTracking()
//                .Pagination<GraphicsCard, GraphicsCardViewModel>(_mapper, page);

//            return motherboardsQuery;
//        }

//        public bool Exists(Guid id)
//        {
//            return DbSet
//                .AsNoTracking()
//                .Where(x => x.Id == id)
//                .Any();
//        }
//    }
//}
