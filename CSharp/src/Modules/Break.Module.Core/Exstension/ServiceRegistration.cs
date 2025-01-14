using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.DLA;
using Modules.Break.Module.Core.Repository;

namespace Modules.Break.Module.Core.Exstension;

public static class ServiceRegistration
{
 //*************************************************Add MySql db *************************************************************//
    public static IServiceCollection AddDbContext(this IServiceCollection serviceDescriptors, IConfiguration configuration)
    {

        serviceDescriptors.AddDbContext<DbInstace>(options =>
            options.UseMySql(
               "Server=localhost;Port=3306;Database=Feature;User=root;Password=password;",
                new MySqlServerVersion(new Version(8, 0, 30))
            ));

        return serviceDescriptors;
    }

//**********************************************Add Core db Services *****************************************************//

    public static IServiceCollection AddBreakCoreServices(this IServiceCollection serviceDescriptors)
    {

        serviceDescriptors.AddScoped<IbreakRepositoryCommand, breakRepositoryCommand>();
        serviceDescriptors.AddScoped<IbreakRepositoryQeury, breakRepositoryQeury>();


        return serviceDescriptors;
    }
//******************************************************************************************************************//
}
