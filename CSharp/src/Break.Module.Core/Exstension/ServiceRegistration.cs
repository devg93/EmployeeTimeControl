using System;
using Break.Module.Core.Astractions;
using Break.Module.Core.DLA;
using Break.Module.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Break.Module.Core.Exstension
{
    public static class ServiceRegistration
    {
         public static IServiceCollection AddDbContext(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
                serviceDescriptors.AddScoped<IbreakRepository, BreakRepository>();
            serviceDescriptors.AddDbContext<DbInstace>(options =>
                options.UseMySql(
                   "Server=localhost;Port=3306;Database=Feature;User=root;Password=password;",
                    new MySqlServerVersion(new Version(8, 0, 30)) 
                ));

            return serviceDescriptors;
        }
        
    }
}