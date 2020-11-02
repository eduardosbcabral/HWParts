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
        Task<IdentityResult> ConfirmEmailAsync(Account account, string code);
        Task<string> GenerateEmailConfirmationTokenAsync(Account account);
    }
}
