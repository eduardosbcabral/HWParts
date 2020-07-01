using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HWParts.Web.Configurations
{
    public class DataSeeds
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var username = "theduardds";
            var email = "theduardds@gmail.com";
            var password = "123456";
            var roles = new string[] { "Super", "Admin", "Moderator", "Common" };

            if (await CreateUser(serviceProvider, email, username, password, roles))
            {
                await AddToRoles(serviceProvider, email, roles);
            }
        }

        private static async Task<bool> CreateUser(IServiceProvider serviceProvider, string email, string username, string password, string[] roles)
        {
            var res = false;

            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<HWPartsDbContext>();

            if (!context.Accounts.Any(u => u.NormalizedUserName == username.ToUpper()))
            {
                var roleStore = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        await roleStore.CreateAsync(new IdentityRole(role)).ConfigureAwait(false);
                    }
                }

                var user = new Account
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true,
                    NormalizedEmail = email.ToUpper(),
                    NormalizedUserName = username.ToUpper(),
                    PhoneNumber = null,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var passwordHash = new PasswordHasher<Account>();
                user.PasswordHash = passwordHash.HashPassword(user, password); ;

                var userStore = new UserStore<Account>(context);
                res = (await userStore.CreateAsync(user).ConfigureAwait(false)).Succeeded;
            }

            return res;
        }

        private static async Task AddToRoles(IServiceProvider serviceProvider, string email, string[] roles)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<Account>>();
            var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false);
            await userManager.AddToRoleAsync(user, roles[0]).ConfigureAwait(false);
        }
    }
}
