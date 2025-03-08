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
        services.AddSingleton<ITimeHenldeLogService, TimeHenldeLogService>();
        services.AddSingleton<IPingSender, PingSender>();


        return services;
    }

    //*******************************Init DI Services Witch Reflection Manuel***************************************************

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

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.FullName))
            .ToArray();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo<ISendServiceToBreakModule>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo<ISendServiceToTimeInTimeOutModule>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
        var registeredTypes = assemblies.SelectMany(a => a.GetTypes())
       .Where(type => typeof(ISendServiceToBreakModule).IsAssignableFrom(type)
                      || typeof(ISendServiceToTimeInTimeOutModule).IsAssignableFrom(type))
       .ToList();

        foreach (var type in registeredTypes)
        {
            Console.WriteLine($"Registered class: {type.FullName}");
        }

        return services;


    }



}
