using HWParts.Core.Domain.Repositories;
using HWParts.Core.Domain.Services;
using HWParts.Core.Infrastructure;
using HWParts.Core.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HWParts.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HWPartsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HWParts")));

            services.AddScoped<HWPartsDbContext>();
            services.AddTransient<IService<SyncronizeProcessorsByPlatform>, SyncronizeProcessorsByPlatform>();
            services.AddTransient<IService<SyncronizeMotherboardsByPlatform>, SyncronizeMotherboardsByPlatform>();
            services.AddTransient<IService<SyncronizeGraphicsCardsByPlatform>, SyncronizeGraphicsCardsByPlatform>();
            services.AddTransient<IService<SyncronizeMemoriesByPlatform>, SyncronizeMemoriesByPlatform>();
            services.AddTransient<IProcessorRepository, ProcessorRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
