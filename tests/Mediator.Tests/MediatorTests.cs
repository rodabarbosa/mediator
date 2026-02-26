using Mediator;
using Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Tests;

// ── Shared test types ─────────────────────────────────────────────────────────

public record PingRequest(string Message) : IRequest<string>;
public record VoidRequest(string Message) : IRequest<bool>;
public record AnotherRequest(int Value) : IRequest<int>;

public class PingEvent : INotification
{
    public string Message { get; init; } = string.Empty;
}

public class AnotherEvent : INotification
{
    public int Value { get; init; }
}

// ── Simple handlers ───────────────────────────────────────────────────────────

public class PingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken = default)
        => Task.FromResult($"Pong: {request.Message}");
}

public class VoidRequestHandler : IRequestHandler<VoidRequest, bool>
{
    public Task<bool> Handle(VoidRequest request, CancellationToken cancellationToken = default)
        => Task.FromResult(true);
}

public class AnotherRequestHandler : IRequestHandler<AnotherRequest, int>
{
    public Task<int> Handle(AnotherRequest request, CancellationToken cancellationToken = default)
        => Task.FromResult(request.Value * 2);
}

// A second handler for PingRequest – used to test the "duplicate" exception.
public class DuplicatePingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken = default)
        => Task.FromResult("Duplicate handler");
}

public class PingEventHandler : INotificationHandler<PingEvent>
{
    public List<string> Received { get; } = new();

    public Task Handle(PingEvent notification, CancellationToken cancellationToken = default)
    {
        Received.Add(notification.Message);
        return Task.CompletedTask;
    }
}

public class AnotherPingEventHandler : INotificationHandler<PingEvent>
{
    public List<string> Received { get; } = new();

    public Task Handle(PingEvent notification, CancellationToken cancellationToken = default)
    {
        Received.Add(notification.Message);
        return Task.CompletedTask;
    }
}

// ── Handler that implements both a request and a notification (simultaneous) ──

public class MultiHandler :
    IRequestHandler<PingRequest, string>,
    INotificationHandler<PingEvent>,
    INotificationHandler<AnotherEvent>
{
    public List<string> Calls { get; } = new();

    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken = default)
    {
        Calls.Add($"request:{request.Message}");
        return Task.FromResult($"Multi: {request.Message}");
    }

    public Task Handle(PingEvent notification, CancellationToken cancellationToken = default)
    {
        Calls.Add($"ping:{notification.Message}");
        return Task.CompletedTask;
    }

    public Task Handle(AnotherEvent notification, CancellationToken cancellationToken = default)
    {
        Calls.Add($"another:{notification.Value}");
        return Task.CompletedTask;
    }
}

// ── Helper ────────────────────────────────────────────────────────────────────

file static class BuildMediator
{
    public static IMediator With(Action<IServiceCollection> configure)
    {
        var services = new ServiceCollection();
        configure(services);
        return services.BuildServiceProvider().GetRequiredService<IMediator>();
    }
}

// ── Tests ─────────────────────────────────────────────────────────────────────

public class RequestHandlerTests
{
    [Fact]
    public async Task Send_Request_ReturnsHandlerResult()
    {
        var mediator = BuildMediator.With(s =>
        {
            s.AddTransient<IMediator, Mediator>();
            s.AddTransient<IRequestHandler<PingRequest, string>, PingHandler>();
        });

        var result = await mediator.Send(new PingRequest("hello"));

        Assert.Equal("Pong: hello", result);
    }

    [Fact]
    public async Task Send_Request_NoHandler_ThrowsInvalidOperationException()
    {
        var mediator = BuildMediator.With(s => s.AddTransient<IMediator, Mediator>());

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => mediator.Send(new PingRequest("hello")));
    }

    [Fact]
    public async Task Send_Request_MultipleHandlers_ThrowsInvalidOperationException()
    {
        var mediator = BuildMediator.With(s =>
        {
            s.AddTransient<IMediator, Mediator>();
            s.AddTransient<IRequestHandler<PingRequest, string>, PingHandler>();
            s.AddTransient<IRequestHandler<PingRequest, string>, DuplicatePingHandler>();
        });

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => mediator.Send(new PingRequest("hello")));
    }

    [Fact]
    public async Task Send_NullRequest_ThrowsArgumentNullException()
    {
        var mediator = BuildMediator.With(s => s.AddTransient<IMediator, Mediator>());

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => mediator.Send((IRequest<string>)null!));
    }
}

public class NotificationHandlerTests
{
    [Fact]
    public async Task Send_Notification_InvokesSingleHandler()
    {
        var handler = new PingEventHandler();

        var mediator = BuildMediator.With(s =>
        {
            s.AddTransient<IMediator, Mediator>();
            s.AddSingleton<INotificationHandler<PingEvent>>(handler);
        });

        await mediator.Send(new PingEvent { Message = "test" });

        Assert.Single(handler.Received);
        Assert.Equal("test", handler.Received[0]);
    }

    [Fact]
    public async Task Send_Notification_InvokesMultipleHandlers()
    {
        var h1 = new PingEventHandler();
        var h2 = new AnotherPingEventHandler();

        var mediator = BuildMediator.With(s =>
        {
            s.AddTransient<IMediator, Mediator>();
            s.AddSingleton<INotificationHandler<PingEvent>>(h1);
            s.AddSingleton<INotificationHandler<PingEvent>>(h2);
        });

        await mediator.Send(new PingEvent { Message = "broadcast" });

        Assert.Single(h1.Received);
        Assert.Single(h2.Received);
    }

    [Fact]
    public async Task Send_Notification_NoHandler_DoesNotThrow()
    {
        var mediator = BuildMediator.With(s => s.AddTransient<IMediator, Mediator>());

        // notifications with no handlers are silently ignored
        var ex = await Record.ExceptionAsync(() => mediator.Send(new PingEvent { Message = "noop" }));

        Assert.Null(ex);
    }

    [Fact]
    public async Task Send_NullNotification_ThrowsArgumentNullException()
    {
        var mediator = BuildMediator.With(s => s.AddTransient<IMediator, Mediator>());

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => mediator.Send((INotification)null!));
    }
}

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddMediator_RegistersIMediator()
    {
        var services = new ServiceCollection();
        services.AddMediator(typeof(PingHandler).Assembly);

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetService<IMediator>();

        Assert.NotNull(mediator);
    }

    [Fact]
    public async Task AddMediator_AssemblyScanning_RegistersRequestHandler()
    {
        var services = new ServiceCollection();
        services.AddMediator(typeof(AnotherRequestHandler).Assembly);

        var mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
        // AnotherRequest has exactly one handler in this assembly
        var result = await mediator.Send(new AnotherRequest(7));

        Assert.Equal(14, result);
    }

    [Fact]
    public async Task AddMediator_AssemblyScanning_RegistersNotificationHandlers()
    {
        var services = new ServiceCollection();
        services.AddMediator(typeof(PingEventHandler).Assembly);

        var mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
        // Should invoke both PingEventHandler and AnotherPingEventHandler without throwing
        var ex = await Record.ExceptionAsync(() => mediator.Send(new PingEvent { Message = "scan" }));

        Assert.Null(ex);
    }
}

public class MultiHandlerTests
{
    [Fact]
    public async Task MultiHandler_HandlesRequestAndMultipleNotificationsSimultaneously()
    {
        var handler = new MultiHandler();

        var mediator = BuildMediator.With(s =>
        {
            s.AddTransient<IMediator, Mediator>();
            s.AddSingleton<IRequestHandler<PingRequest, string>>(handler);
            s.AddSingleton<INotificationHandler<PingEvent>>(handler);
            s.AddSingleton<INotificationHandler<AnotherEvent>>(handler);
        });

        var requestResult = await mediator.Send(new PingRequest("hello"));
        await mediator.Send(new PingEvent { Message = "world" });
        await mediator.Send(new AnotherEvent { Value = 42 });

        Assert.Equal("Multi: hello", requestResult);
        Assert.Contains("request:hello", handler.Calls);
        Assert.Contains("ping:world", handler.Calls);
        Assert.Contains("another:42", handler.Calls);
    }
}
