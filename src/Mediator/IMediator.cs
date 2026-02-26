namespace Mediator;

/// <summary>
/// Defines the central mediator that dispatches requests and notifications to their respective handlers.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Dispatches a request to its single registered <see cref="IRequestHandler{TRequest, TResponse}"/> and
    /// returns the handler's response.
    /// </summary>
    /// <typeparam name="TResponse">The type of value returned by the handler.</typeparam>
    /// <param name="request">The request object to dispatch. Must not be <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A task that resolves to the response produced by the handler.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="request"/> is <see langword="null"/>.</exception>
    /// <exception cref="System.InvalidOperationException">
    /// Thrown when no handler or more than one handler is registered for the request type.
    /// </exception>
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Broadcasts a notification to all registered <see cref="INotificationHandler{TNotification}"/> implementations.
    /// If no handlers are registered the method completes without error.
    /// </summary>
    /// <param name="notification">The notification object to broadcast. Must not be <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the completion of all notification handlers.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="notification"/> is <see langword="null"/>.</exception>
    Task Send(INotification notification, CancellationToken cancellationToken = default);
}
