using HWParts.Core.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;

namespace HWParts.Tests.FakeObjects
{
    public class FakeSignInManager : SignInManager<Account>
    {
        public FakeSignInManager()
                : base(new FakeUserManager(),
                     new Mock<IHttpContextAccessor>().Object,
                     new Mock<IUserClaimsPrincipalFactory<Account>>().Object,
                     new Mock<IOptions<IdentityOptions>>().Object,
                     new Mock<ILogger<SignInManager<Account>>>().Object,
                     new Mock<IAuthenticationSchemeProvider>().Object,
                     new Mock<IUserConfirmation<Account>>().Object)
        { }
    }



    public class FakeUserManager : UserManager<Account>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<Account>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<Account>>().Object,
              new IUserValidator<Account>[0],
              new IPasswordValidator<Account>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<Account>>>().Object)
        { }

        public override Task<IdentityResult> CreateAsync(Account user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> AddToRoleAsync(Account user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<string> GenerateEmailConfirmationTokenAsync(Account user)
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }

    }
}
