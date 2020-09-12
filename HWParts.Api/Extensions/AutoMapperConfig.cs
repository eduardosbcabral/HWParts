using AutoMapper;
using HWParts.Core.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HWParts.Web.Extensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
        }
    }
}
