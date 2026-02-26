namespace Mediator;

/// <summary>
/// Defines a handler for a notification of type <typeparamref name="TNotification"/>.
/// Multiple handlers may be registered for the same notification type; all of them are
/// invoked sequentially when the notification is sent via <see cref="IMediator.Send(INotification, System.Threading.CancellationToken)"/>.
/// </summary>
/// <typeparam name="TNotification">The type of notification being handled. Must implement <see cref="INotification"/>.</typeparam>
public interface INotificationHandler<TNotification>
    where TNotification : INotification
{
    /// <summary>
    /// Handles the given <paramref name="notification"/>.
    /// </summary>
    /// <param name="notification">The notification object to process.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous handling of the notification.</returns>
    Task Handle(TNotification notification, CancellationToken cancellationToken = default);
}
