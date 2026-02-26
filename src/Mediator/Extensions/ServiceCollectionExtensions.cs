using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Extensions;

/// <summary>
/// Provides extension methods on <see cref="IServiceCollection"/> to register the mediator
/// and automatically discover handler implementations via assembly scanning.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="IMediator"/> and scans the given <paramref name="assemblies"/> for all
    /// concrete <see cref="IRequestHandler{TRequest,TResponse}"/> and
    /// <see cref="INotificationHandler{TNotification}"/> implementations, adding each as a transient service.
    /// </summary>
    /// <param name="services">The service collection to add registrations to.</param>
    /// <param name="assemblies">
    /// One or more assemblies to scan for handler types.
    /// If no assemblies are provided only <see cref="IMediator"/> itself is registered.
    /// </param>
    /// <returns>The same <paramref name="services"/> instance, to allow method chaining.</returns>
    public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddTransient<IMediator, Mediator>();

        foreach (var assembly in assemblies)
        {
            RegisterHandlers(services, assembly);
        }

        return services;
    }

    /// <summary>
    /// Scans all non-abstract classes in <paramref name="assembly"/> and registers any
    /// <see cref="IRequestHandler{TRequest,TResponse}"/> or <see cref="INotificationHandler{TNotification}"/>
    /// interface implementations as transient services.
    /// </summary>
    /// <param name="services">The service collection to add registrations to.</param>
    /// <param name="assembly">The assembly to scan for handler types.</param>
    private static void RegisterHandlers(IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract);

        foreach (var type in types)
        {
            var requestHandlerInterfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            foreach (var interfaceType in requestHandlerInterfaces)
            {
                services.AddTransient(interfaceType, type);
            }

            var notificationHandlerInterfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>));

            foreach (var interfaceType in notificationHandlerInterfaces)
            {
                services.AddTransient(interfaceType, type);
            }
        }
    }
}
