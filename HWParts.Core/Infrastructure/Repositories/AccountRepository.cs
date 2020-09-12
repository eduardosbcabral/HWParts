using AutoMapper;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly IMapper _mapper;

        public AccountRepository(
            HWPartsDbContext context,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IMapper mapper)
            : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateAsync(Account obj, string password)
        {
            return await _userManager.CreateAsync(obj, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(Account obj, string role)
        {
            return await _userManager.AddToRoleAsync(obj, role);
        }

        public async Task<SignInResult> PasswordSignInAsync(string username, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(
                username,
                password,
                rememberMe,
                lockoutOnFailure: false
            );
        }

        public async Task<Account> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<SafeAccountViewModel> FindByNameAsyncSafe(string username)
        {
            return _mapper.Map<SafeAccountViewModel>(await _userManager.FindByNameAsync(username));
        }
    }
}
