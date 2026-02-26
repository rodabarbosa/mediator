using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator;

/// <summary>
/// Default implementation of <see cref="IMediator"/> that resolves handlers from an
/// <see cref="IServiceProvider"/> and dispatches requests and notifications accordingly.
/// </summary>
public class Mediator : IMediator
{
    /// <summary>
    /// Cache that maps a handler interface type to its resolved <see cref="MethodInfo"/> for
    /// the <c>Handle</c> method, avoiding repeated reflection lookups after the first call.
    /// </summary>
    private static readonly ConcurrentDictionary<Type, MethodInfo> _methodCache = new();

    /// <summary>
    /// The service provider used to resolve handler instances at runtime.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of <see cref="Mediator"/> with the given service provider.
    /// </summary>
    /// <param name="serviceProvider">
    /// The <see cref="IServiceProvider"/> from which request and notification handlers are resolved.
    /// </param>
    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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
