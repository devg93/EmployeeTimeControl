using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 using Modules.TimeInTimeOut.Module.Core.DAL.Exstension;


namespace TimeInTimeOut.Module.Api;

 public static class TimeInTimeOutModule
 {
         public static IServiceCollection RegisterTimeInTimeOutModule(this IServiceCollection services)
        {
            services.AddTimeInTimeOutDbContext(); 
            services.AddTimeInTimeOutCoreServicesDb();
            services.AddTimeInTimeOutCoreServicesWorker();
            
        
            return services;
        }
 }
    
