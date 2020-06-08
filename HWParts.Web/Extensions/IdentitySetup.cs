using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure;
using HWParts.Core.Infrastructure.Identity.Authorization;
using HWParts.Core.Infrastructure.Identity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HWParts.Web.Extensions
{
    public static class IdentitySetup
    {
        public static void AddIdentitySetup(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddDefaultIdentity<Account>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<HWPartsDbContext>();
        }
        public static void AddAuthSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddAuthentication();
            //.AddFacebook(o =>
            //{
            //    o.AppId = configuration["Authentication:AppId"];
            //    o.AppSecret = configuration["Authentication:AppSecret"];
            //})
            //.AddGoogle(o =>
            //{
            //    o.ClientId = configuration["Authentication:Google:ClientId"];
            //    o.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            //});

            services.AddAuthorization(o =>
            {
                o.AddPolicy("CanWriteComponentData", policy => 
                    policy.Requirements.Add(new ClaimRequirement(UserClaims.Components, UserClaimValues.Write)));
                o.AddPolicy("CanRemoveComponentData", policy => 
                    policy.Requirements.Add(new ClaimRequirement(UserClaims.Components, UserClaimValues.Remove)));
            });
        }
    }

}
