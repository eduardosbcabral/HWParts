using HWParts.Core.Infrastructure.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HWParts.Web.Extensions
{
    public static class EmailSetup
    {
        public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
