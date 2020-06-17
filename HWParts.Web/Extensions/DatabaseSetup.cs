using HWParts.Core.Infrastructure;
using HWParts.Core.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HWParts.Web.Extensions
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

#if DEBUG
            services.AddDbContext<HWPartsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("HWParts")));
#else
            services.AddDbContext<HWPartsDbContext>(opt => opt.UseInMemoryDatabase("HWParts"));
#endif
        }
    }
}
