

using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService;

namespace Modules.TimeInTimeOut.Module.Core.DAL.Exstension;
public static class ServiceRegistracionTimeInTimeOutCore
{
    //*************************************************Add MySql db ********************************************************//
    public static IServiceCollection AddTimeInTimeOutDbContext(this IServiceCollection serviceDescriptors)
    {

        serviceDescriptors.AddDbContext<DbInstaceTimeInOut>(options =>
       options.UseMySql(
          "Server=localhost;Port=3306;Database=TimeInTimeOut;User=root;Password=password;",
           new MySqlServerVersion(new Version(8, 0, 30))
       ));


        return serviceDescriptors;
    }
    //**********************************************Add Core db Services ************************************************//
    public static IServiceCollection AddTimeInTimeOutCoreServicesDb(this IServiceCollection serviceDescriptors)
    {


        serviceDescriptors.AddScoped<IcomingAndgoingRepositoryCommand, ComingAndgoingRepository>();
        serviceDescriptors.AddScoped<IcomingAndgoingRepositoryQeury, ComingAndgoingRepository>();

        return serviceDescriptors;
    }

    //*******************************************Add BackgroundService Services *****************************************************//
    public static IServiceCollection AddTimeInTimeOutCoreServicesWorker(this IServiceCollection serviceDescriptors)
    {

        serviceDescriptors.AddHostedService<TimeInTimeOutWorkerCommand>();
        serviceDescriptors.AddScoped<ITimeInTimeOutWorkerHendle, TimeInTimeOutWorkerHendle>();
        serviceDescriptors.AddScoped<ITimeInTimeOutMediator, TimeInTimeOutMediator>();
        serviceDescriptors.AddScoped<IAggregatorServiceTimeInTimeOut, AggregatorServiceTimeInTimeOut>();
        serviceDescriptors.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        return serviceDescriptors;
    }

}

