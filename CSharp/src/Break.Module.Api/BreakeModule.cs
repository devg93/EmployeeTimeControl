

using Break.Module.Core.Exstension;
using Microsoft.Extensions.DependencyInjection;

namespace Break.Module.Api
{
    public static class BreakeModule
    {
        public static IServiceCollection RegisterModuleBreak(this IServiceCollection services)
        {
          services.AddDbContextServicesBreake();
            return services;
        }
        
    }
}