using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Data.Repositories;

namespace UnicornInsurance.Data
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UnicornDataDBContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("UnicornDataDB")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IWeaponRepository, WeaponRepository>();
            services.AddScoped<IMobileSuitRepository, MobileSuitRepository>();
            services.AddScoped<IMobileSuitCartRepository, MobileSuitCartRepository>();

            return services;
        }
    }
}
