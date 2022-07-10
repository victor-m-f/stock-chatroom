using MediatR;
using StockChatroom.Shared.Extensions;

namespace StockChatroom.Shared.Events;

public class EventBase : INotification
{
    #region Properties

    public string Name { get; }
    public DateTime Date { get; }

    #endregion

    #region Constructors

    public EventBase()
    {
        Date = DateTime.Now;
        Name = GetEventName(GetType());
    }

    #endregion

    public static string GetEventName<TEvent>()
        where TEvent : EventBase, new() =>
        GetEventName(typeof(TEvent));

    public static string GetEventName(Type type) =>
        type.Name
        .RemoveText("Event")
        .RemoveText("Integration")
        .SeparateCamelCase()
        .ToLower();
}
