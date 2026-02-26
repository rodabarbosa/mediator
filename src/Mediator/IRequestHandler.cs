namespace Mediator;

/// <summary>
/// Defines a handler for a request of type <typeparamref name="TRequest"/> that produces a
/// response of type <typeparamref name="TResponse"/>.
/// Only one handler may be registered per request type; registering more than one will cause
/// <see cref="IMediator.Send{TResponse}"/> to throw an <see cref="System.InvalidOperationException"/>.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled. Must implement <see cref="IRequest{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">The type of value produced by the handler.</typeparam>
public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the given <paramref name="request"/> and returns the response.
    /// </summary>
    /// <param name="request">The request object to process.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A task that resolves to the response produced by handling the request.</returns>
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
}
