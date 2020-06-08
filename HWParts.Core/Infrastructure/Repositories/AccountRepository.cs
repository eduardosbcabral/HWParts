using AutoMapper;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly IMapper _mapper;

        public AccountRepository(
            HWPartsDbContext context,
            IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public PaginationObject<AccountViewModel> ListPaginated(int? page)
        {
            return Db
                .Users
                .AsNoTracking()
                .Pagination<Account, AccountViewModel>(_mapper, page);
        }
    }
}
