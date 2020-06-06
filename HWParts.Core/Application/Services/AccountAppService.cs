using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        public AccountAppService(
            IMapper mapper,
            IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            Bus = mediatorHandler;
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
    }
}
