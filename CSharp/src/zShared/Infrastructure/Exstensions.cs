
using Microsoft.Extensions.DependencyInjection;
using zShared.Mediator;

namespace Infrastructure
{
    public static class Exstensions
    {
        public static  IServiceCollection addInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IMediatorGetService, MediatorGetService>();
            return services;
        }

    }
}