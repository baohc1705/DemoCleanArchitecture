using AutoMapper;
using DemoCleanArchitecture.Application.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => {},Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}
