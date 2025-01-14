using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Modules.Break.Module.Core.Exstension;

namespace Modules.Break.Module.Api;

    public static class BreakeModule
    {
        public static IServiceCollection RegisterModuleBreak(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration); 
            return services;
        }
    }

