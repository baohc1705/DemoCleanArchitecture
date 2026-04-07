using DemoCleanArchitecture.Domain.Interfaces;
using DemoCleanArchitecture.Infrastructure.Data;
using DemoCleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration) {
            // inject automapper
            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
            // Kết nối SQL server
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("MenuNewsDB"));
            });
            // inject repository dependency
            services.AddScoped<IMenuRepository, MenuRepository>();

            services.AddScoped<INewsRepository, NewsRepository>();

            return services; 
        }
    }
}
