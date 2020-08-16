using HWParts.Core.Domain.Configurations;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace HWParts.Web.Extensions
{
    public static class IdentitySetup
    {
        public static void AddIdentitySetup(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddIdentity<Account, IdentityRole>(options =>
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

            var key = Encoding.ASCII.GetBytes(JwtSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("SuperUserOnly", policy =>
                    policy.RequireRole("Super"));
                o.AddPolicy("AdminUserOnly", policy =>
                    policy.RequireRole("Admin"));
                o.AddPolicy("ModeratorUserOnly", policy =>
                    policy.RequireRole("Moderator"));
                o.AddPolicy("CommonUserOnly", policy =>
                    policy.RequireRole("Common"));
                o.AddPolicy("IsAuthenticated", policy =>
                    policy.RequireAuthenticatedUser());
            });
        }
    }

}
