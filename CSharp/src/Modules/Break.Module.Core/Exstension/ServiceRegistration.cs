using System;
using Break.Module.Core.Astractions.Dbcontracts;
using Break.Module.Core.BreakWorker.BackgroundService;
using Break.Module.Core.BreakWorker.OrchestratorService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Break.Module.Core.Astractions.Dbcontracts;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Modules.Break.Module.Core.Astractions.Iservices;
using Modules.Break.Module.Core.BreakWorker.Command;
using Modules.Break.Module.Core.DLA;
using Modules.Break.Module.Core.Iservices;
using Modules.Break.Module.Core.Mediator;
using Modules.Break.Module.Core.Repository;
using Modules.Break.Module.Core.Repository.Busy;


namespace Modules.Break.Module.Core.Exstension;

public static class ServiceRegistration
{
    //*************************************************Add MySql db ********************************************************//
    public static IServiceCollection AddDbContext(this IServiceCollection serviceDescriptors, IConfiguration configuration)
    {

        serviceDescriptors.AddDbContext<DbInstace>(options =>
            options.UseMySql(
               "Server=localhost;Port=3306;Database=Feature;User=root;Password=password;",
                new MySqlServerVersion(new Version(8, 0, 30))
            ));
            

        return serviceDescriptors;
    }

    //**********************************************Add Core db Services ************************************************//

    public static IServiceCollection AddBreakCoreServices(this IServiceCollection serviceDescriptors)
    {

        serviceDescriptors.AddScoped<IbreakRepositoryCommand, breakRepositoryCommand>();
        serviceDescriptors.AddScoped<IbreakRepositoryQeury, breakRepositoryQeury>();
        serviceDescriptors.AddScoped<IbusyRepositoryCommand, busyRepositoryCommand>();
        serviceDescriptors.AddScoped<IbusyRepositoryQeury, busyRepositoryQeury>();
        serviceDescriptors.AddTransient<IRepositoryContract, RepositoryContractImplementation>();


        return serviceDescriptors;
    }
    //*******************************************Add Worker Services *****************************************************//

     public static IServiceCollection AddBreakWorkerServices(this IServiceCollection serviceDescriptors)
    {

        serviceDescriptors.AddSingleton<WorkerCommand>();
        serviceDescriptors.AddScoped<IWorkerHenlde,WorkerHenlde>();
        serviceDescriptors.AddScoped<IBreakTimeUpdateMediator, BreakTimeUpdateMediator>();
        serviceDescriptors.AddScoped<IAggregatorServiceBrakeTime, AggregatorServiceBrakeTime>();
        return serviceDescriptors;
    }
}
