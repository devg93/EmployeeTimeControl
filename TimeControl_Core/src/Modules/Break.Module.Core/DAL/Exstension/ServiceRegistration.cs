


using Break.Module.Core.BreakWorker.CommonServices.OrchestratorService;

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

        return serviceDescriptors;
    }
    //*******************************************Add BackgroundService Services *****************************************************//

    public static IServiceCollection AddBreakWorkerServices(this IServiceCollection serviceDescriptors)
    {

        serviceDescriptors.AddHostedService<BreakWorkerCommand>();
        serviceDescriptors.AddScoped<IWorkerHenlde, WorkerHenlde>();
        serviceDescriptors.AddScoped<IServicesFactory, ServicesFactory>();
        serviceDescriptors.AddScoped<IServicesFacade,IServicesFacade>();
        serviceDescriptors.AddScoped<IOrchestratorService, OrchestratorService>();
        serviceDescriptors.AddScoped<IPersistenceService, PersistenceService>();
        serviceDescriptors.AddScoped<ITimeValidator, TimeValidator>();
    
        // serviceDescriptors.addSharedServices();


        return serviceDescriptors;
    }
}
