using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Modules.Break.Module.Core.Exstension;
using Modules.Break.Module.Core.Exstension.DAL;
using TimeInTimeOut.Module.Core.Exstension;
using TimeInTimeOut.Module.Core.Exstension.DAL;

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

         public static IServiceCollection RegisterTimeInTimeOutModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTimeInTimeOutDbContext(); 
            services.AddTimeInTimeOutCoreServicesDb();
            services.AddTimeInTimeOutCoreServicesWorker();
            
            
        
            return services;
        }

    }

