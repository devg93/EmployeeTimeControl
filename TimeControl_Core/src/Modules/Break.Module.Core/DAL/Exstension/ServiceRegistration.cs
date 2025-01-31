using System;
using Break.Module.Core.BreakWorker.BackgroundService;
using Break.Module.Core.BreakWorker.OrchestratorService;
using Break.Module.Core.ServicesCommunication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Modules.Break.Module.Core.Astractions.Iservices;
using Modules.Break.Module.Core.BreakWorker.Command;
using Modules.Break.Module.Core.DAL;
using Modules.Break.Module.Core.Iservices;
using Modules.Break.Module.Core.Mediator;
using Modules.Break.Module.Core.Repository;
using Modules.Break.Module.Core.Repository.Busy;
using Modules.Break.Module.Core.Repository.Busy.DAL;
using Modules.Break.Module.Core.Repository.DAL;
using Shared;
using Shared.Services.ModuleCommunication.Contracts;


namespace Modules.Break.Module.Core.Exstension.DAL;

public static class ServiceRegistration
{
    //*************************************************Add MySql db ********************************************************//
    public static IServiceCollection AddBreakDbContext(this IServiceCollection serviceDescriptors, IConfiguration configuration)
    {

        serviceDescriptors.AddDbContext<DbInstace>(options =>
            options.UseMySql(
               "Server=localhost;Port=3306;Database=Break;User=root;Password=password;",
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
        // serviceDescriptors.AddScoped<IRepositoryContract, RepositoryContractImplementation>();

        return serviceDescriptors;
    }
    //*******************************************Add BackgroundService Services *****************************************************//

     public static IServiceCollection AddBreakWorkerServices(this IServiceCollection serviceDescriptors)
    {

        serviceDescriptors.AddHostedService<BreakWorkerCommand>();
        serviceDescriptors.AddScoped<IWorkerHenlde,WorkerHenlde>();
        serviceDescriptors.AddScoped<IBreakTimeUpdateMediator, BreakTimeUpdateMediator>();
        serviceDescriptors.AddScoped<IAggregatorServiceBrakeTime, AggregatorServiceBrakeTime>();
        // serviceDescriptors.addSharedServices();
    

        return serviceDescriptors;
    }
}
