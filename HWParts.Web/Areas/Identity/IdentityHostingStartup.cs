using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(HWParts.Web.Areas.Identity.IdentityHostingStartup))]
namespace HWParts.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
