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
        private readonly IMapper _mapper;

        public UserManager<Account> UserManager { get; }
        public SignInManager<Account> SignInManager { get; }

        public AccountRepository(
            HWPartsDbContext context,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IMapper mapper)
            : base(context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateAsync(Account obj, string password)
        {
            return await UserManager.CreateAsync(obj, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(Account obj, string role)
        {
            return await UserManager.AddToRoleAsync(obj, role);
        }

        public async Task<SignInResult> PasswordSignInAsync(string username, string password, bool rememberMe)
        {
            return await SignInManager.PasswordSignInAsync(
                username,
                password,
                rememberMe,
                lockoutOnFailure: false
            );
        }

        public async Task<Account> FindByNameAsync(string username)
        {
            return await UserManager.FindByNameAsync(username);
        }

        public async Task<Account> FindByIdAsync(string id)
        {
            return await UserManager.FindByIdAsync(id);
        }

        public async Task<SafeAccountDTO> FindByNameAsyncSafe(string username)
        {
            return _mapper.Map<SafeAccountDTO>(await UserManager.FindByNameAsync(username));
        }

        public async Task<IdentityResult> ConfirmEmailAsync(Account account, string code)
        {
            return await UserManager.ConfirmEmailAsync(account, code);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Account account)
        {
            return await UserManager.GenerateEmailConfirmationTokenAsync(account);
        }

        public async Task<Account> FindByEmailAsync(string email)
        {
            return await UserManager.FindByEmailAsync(email);
        }

        public bool RequireConfirmedAccount()
        {
            return UserManager.Options.SignIn.RequireConfirmedAccount;
        }

        public async Task<bool> IsEmailConfirmedAsync(Account user)
        {
            return await UserManager.IsEmailConfirmedAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Account user)
        {
            return await UserManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(Account user, string code, string password)
        {
            return await UserManager.ResetPasswordAsync(user, code, password);
        }
    }
}
