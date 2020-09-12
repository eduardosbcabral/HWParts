using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IdentityResult> CreateAsync(Account obj, string password);
        Task<IdentityResult> AddToRoleAsync(Account obj, string role);
        Task<SignInResult> PasswordSignInAsync(string username, string password, bool rememberMe);
        Task<Account> FindByNameAsync(string username);

        Task<SafeAccountViewModel> FindByNameAsyncSafe(string username);
    }
}
