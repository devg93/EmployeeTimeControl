
using Microsoft.Extensions.DependencyInjection;
using zShared.Mediator;
using zShared.Services.Tasks.PingCheker;
using zShared.Services.Tasks.ShedulerTuplelog;

namespace Infrastructure
{
    public static class Exstensions
    {
        public static IServiceCollection addSharedServices(this IServiceCollection services)
        {
            services.AddScoped<ITimeHenldeLogService, TimeHenldeLogService>();
            services.AddScoped<IPingSender, PingSender>();
         
            return services;
        }

    }
}