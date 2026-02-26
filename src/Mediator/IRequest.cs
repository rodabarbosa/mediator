namespace Mediator;

/// <summary>
/// Marker interface for a request that returns a response of type <typeparamref name="TResponse"/>.
/// Each request may be handled by exactly one <see cref="IRequestHandler{TRequest, TResponse}"/>.
/// </summary>
/// <typeparam name="TResponse">The type of value returned by the request handler.</typeparam>
public interface IRequest<TResponse> { }
