
using Microsoft.Extensions.DependencyInjection;
using Shared.Services.ModuleCommunication.Contracts;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.Repository;
using TimeInTimeOut.Module.Core.Services;

namespace TimeInTimeOut.Module.Core.Exstension
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddTimeInTimeOutCoreServices(this IServiceCollection serviceDescriptors)
        {

            serviceDescriptors.AddScoped<IGetServiceFtomTimeInTimeOutById, SendDataToBreakModule>();
            serviceDescriptors.AddScoped<IcomingAndgoingRepository, ComingAndgoingRepository>();

            return serviceDescriptors;
        }

    }
}