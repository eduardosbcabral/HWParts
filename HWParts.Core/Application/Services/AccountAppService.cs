using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        private readonly IAccountRepository _accountRepository;

        public AccountAppService(
            IMapper mapper,
            IMediatorHandler mediatorHandler,
            IAccountRepository accountRepository)
        {
            _mapper = mapper;
            Bus = mediatorHandler;
            _accountRepository = accountRepository;
        }
        public Task Register(RegisterAccountViewModel registerAccountViewModel)
        {
            var command = _mapper.Map<RegisterAccountCommand>(registerAccountViewModel);
            return Bus.SendCommand(command);
        }

        public Task Login(LoginAccountViewModel loginAccountViewModel)
        {
            var command = _mapper.Map<LoginAccountCommand>(loginAccountViewModel);
            return Bus.SendCommand(command);
        }

        public PaginationObject<AccountViewModel> ListPaginated(int? page)
        {
            return _accountRepository.ListPaginated(page);
        }
    }
}
