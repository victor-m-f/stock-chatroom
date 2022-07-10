using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StockChatroom.Domain.Services;
using StockChatroom.Shared.Events;
using System.Text.Json;

namespace StockChatroom.Application.Services.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly HashSet<Type> _eventsToListen = new ()
    {
        typeof(CommandSentEvent),
        typeof(MessageSentEvent),
        typeof(ChatRoomCreatedEvent),
    };
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task ProcessEvent(string eventMessage, CancellationToken cancellationToken)
    {
        var @event = ConvertToEvent(eventMessage);
        ArgumentNullException.ThrowIfNull(@event);

        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Publish(@event, cancellationToken);
    }

    private object? ConvertToEvent(string eventMessage)
    {
        var @event = JsonSerializer.Deserialize<GenericEvent>(eventMessage);

        ArgumentNullException.ThrowIfNull(@event);

        var eventType = _eventsToListen.FirstOrDefault(x => EventBase.GetEventName(x) == @event.Name);
        ArgumentNullException.ThrowIfNull(eventType);

        return JsonSerializer.Deserialize(eventMessage, eventType);
    }
}