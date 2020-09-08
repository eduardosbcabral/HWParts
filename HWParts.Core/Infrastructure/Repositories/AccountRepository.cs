using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly UserManager<Account> _userManager;

        public AccountRepository(
            HWPartsDbContext context,
            UserManager<Account> userManager)
            : base(context)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(Account obj, string password)
        {
            return await _userManager.CreateAsync(obj, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(Account obj, string role)
        {
            return await _userManager.AddToRoleAsync(obj, role);
        }
    }
}
