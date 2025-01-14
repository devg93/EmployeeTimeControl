
using Microsoft.Extensions.DependencyInjection;
using Shared.Mediator;
using Shared.Services.Tasks.PingCheker;
using Shared.Services.Tasks.ShedulerTuplelog;

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