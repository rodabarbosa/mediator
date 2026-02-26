using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator;

public class Mediator : IMediator
{
    private static readonly ConcurrentDictionary<Type, MethodInfo> _methodCache = new();

    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handlers = _serviceProvider.GetServices(handlerType).ToList();

        if (handlers.Count > 1)
            throw new InvalidOperationException(
                $"More than one handler found for request type '{requestType.Name}'. Only a single request handler is allowed.");

        if (handlers.Count == 0)
            throw new InvalidOperationException(
                $"No handler found for request type '{requestType.Name}'.");

        var method = _methodCache.GetOrAdd(handlerType, t => t.GetMethod("Handle")!);
        return await (Task<TResponse>)method.Invoke(handlers[0], [request, cancellationToken])!;
    }

    public async Task Send(INotification notification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(notification);

        var notificationType = notification.GetType();
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);

        var handlers = _serviceProvider.GetServices(handlerType);
        var method = _methodCache.GetOrAdd(handlerType, t => t.GetMethod("Handle")!);

        foreach (var handler in handlers)
        {
            await (Task)method.Invoke(handler, [notification, cancellationToken])!;
        }
    }
}
