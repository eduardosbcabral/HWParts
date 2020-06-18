using HWParts.Core.Infrastructure;
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

            services.AddDbContext<HWPartsDbContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("HWPartsSqlite"));
            });
        }
    }
}
