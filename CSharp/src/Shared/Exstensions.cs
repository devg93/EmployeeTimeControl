
using Microsoft.Extensions.DependencyInjection;
using Shared.Mediator;
using Shared.Services.Tasks.PingCheker;
using Shared.Services.Tasks.ShedulerTuplelog;

namespace Shared;

    public static class Exstensions
    {
        public static IServiceCollection addSharedServices(this IServiceCollection services)
        {
            services.AddScoped<ITimeHenldeLogService, TimeHenldeLogService>();
            services.AddScoped<IPingSender, PingSender>();
            services.AddScoped<IMediatorGetService,MediatorGetServices>();
         
            return services;
        }

    }
