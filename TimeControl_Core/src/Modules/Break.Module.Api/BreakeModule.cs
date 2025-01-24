using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Modules.Break.Module.Core.Exstension;
using Modules.Break.Module.Core.Exstension.DAL;

namespace Modules.Break.Module.Api;

    public static class BreakeModule
    {
        public static IServiceCollection RegisterModuleBreak(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBreakDbContext(configuration); 
            services.AddBreakCoreServices();
            services.AddBreakWorkerServices();
            return services;
        }
    }

