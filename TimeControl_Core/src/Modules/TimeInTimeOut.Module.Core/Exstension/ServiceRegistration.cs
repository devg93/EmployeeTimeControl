
using Microsoft.Extensions.DependencyInjection;
using Shared.Services.ModuleCommunication.Contracts;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.Repository;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService;
using TimeInTimeOut.Module.Core.Services;

namespace TimeInTimeOut.Module.Core.Exstension
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddTimeInTimeOutCoreServicesDb(this IServiceCollection serviceDescriptors)
        {

            serviceDescriptors.AddScoped<ISendDataToBreakModule, SendDataToBreakModule>();
            serviceDescriptors.AddScoped<IcomingAndgoingRepository, ComingAndgoingRepository>();

            return serviceDescriptors;
        }


        public static IServiceCollection AddTimeInTimeOutCoreServicesWorker(this IServiceCollection serviceDescriptors)
        {

            serviceDescriptors.AddHostedService<TimeInTimeOutWorkerCommand>();
            serviceDescriptors.AddScoped<ITimeInTimeOutWorkerHendle, TimeInTimeOutWorkerHendle>();
            serviceDescriptors.AddScoped<ITimeInTimeOutMediator, TimeInTimeOutMediator>();
            serviceDescriptors.AddScoped<IAggregatorServiceTimeInTimeOut, AggregatorServiceTimeInTimeOut>();

            return serviceDescriptors;
        }

    }
}