using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        UserManager<Account> UserManager { get; }
        SignInManager<Account> SignInManager { get; }
        
        Task<IdentityResult> CreateAsync(Account obj, string password);
        Task<IdentityResult> AddToRoleAsync(Account obj, string role);
        Task<SignInResult> PasswordSignInAsync(string username, string password, bool rememberMe);
        Task<Account> FindByNameAsync(string username);
        Task<Account> FindByIdAsync(string id);
        Task<SafeAccountDTO> FindByNameAsyncSafe(string username);
        Task<Account> FindByEmailAsync(string email);
        Task<IdentityResult> ConfirmEmailAsync(Account account, string code);
        Task<string> GenerateEmailConfirmationTokenAsync(Account account);
        Task<bool> IsEmailConfirmedAsync(Account user);
        Task<string> GeneratePasswordResetTokenAsync(Account user);
        Task<IdentityResult> ResetPasswordAsync(Account user, string code, string password);

        // UserManager attributes
        bool RequireConfirmedAccount();
    }
}
