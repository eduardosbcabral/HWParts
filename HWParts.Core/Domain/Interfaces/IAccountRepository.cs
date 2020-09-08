using HWParts.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IdentityResult> CreateAsync(Account obj, string password);
        Task<IdentityResult> AddToRoleAsync(Account obj, string role);
    }
}
