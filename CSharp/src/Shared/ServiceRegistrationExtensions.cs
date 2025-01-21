using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services.ModuleCommunication.Contracts;
using Shared.Services.Tasks.PingCheker;
using Shared.Services.Tasks.ShedulerTuplelog;

namespace Shared;

public static class ServiceRegistrationExtensions
{
    //**********************************************************************************


    public static IServiceCollection addSharedServices(this IServiceCollection services)
    {
        services.AddScoped<ITimeHenldeLogService, TimeHenldeLogService>();
        services.AddScoped<IPingSender, PingSender>();
        

        return services;
    }

    //*******************************Init DI Services Witch Reflection***************************************************

    public static IServiceCollection AddServicesByInterface<TInterface>(this IServiceCollection services, Assembly assembly,
    ServiceLifetime lifetime = ServiceLifetime.Scoped
    )
    {
        var implementations = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(TInterface).IsAssignableFrom(t));

        foreach (var implementation in implementations)
        {
            services.Add(new ServiceDescriptor(typeof(TInterface), implementation, lifetime));
        }

        return services;
    }

    //**********************************Init DI Services Witch Scrutor*************************************************

    public static IServiceCollection AddServicesRegisterByInterface(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(classes =>
                    classes
                        .AssignableTo<IGetServiceFromBreakById>()
                        )
                        .AddClasses(classes =>
                            classes.AssignableTo<IGetServiceFtomTimeInTimeOutById>()
                )
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }
}
