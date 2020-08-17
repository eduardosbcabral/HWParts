//using AutoMapper;
//using HWParts.Core.Application.ViewModels.PowerSupply;
//using HWParts.Core.Domain.Entities;
//using HWParts.Core.Domain.Interfaces;
//using HWParts.Core.Domain.Queries;
//using HWParts.Core.Infrastructure.Common.Pagination;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;

//namespace HWParts.Core.Infrastructure.Repositories
//{
//    public class PowerSupplyRepository : Repository<PowerSupply>, IPowerSupplyRepository
//    {
//        private readonly IMapper _mapper;

//        public PowerSupplyRepository(
//            HWPartsDbContext context,
//            IMapper mapper) 
//            : base(context)
//        {
//            _mapper = mapper;
//        }

//        public PowerSupply GetByPlatformId(string platformId)
//        {
//            return DbSet
//                .AsNoTracking()
//                .FirstOrDefault(PowerSupplyQueries.GetByPlatformId(platformId));
//        }

//        public PaginationObject<PowerSupplyViewModel> ListPaginated(int? page)
//        {
//            return DbSet
//                .AsNoTracking()
//                .Pagination<PowerSupply, PowerSupplyViewModel>(_mapper, page);
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
