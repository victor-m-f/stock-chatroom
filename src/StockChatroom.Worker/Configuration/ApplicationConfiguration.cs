using MediatR;
using StockChatroom.Application.Configuration.AppSettings;
using StockChatroom.Application.Services.Hubs;
using StockChatroom.Application.Services.Stooq;
using StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;
using StockChatroom.Application.UseCases.Messages.ProccessCommand;
using StockChatroom.Application.UseCases.Messages.SendMessage;
using StockChatroom.Shared.Events;
using EventProcessing = StockChatroom.Application.Services.EventProcessing;

namespace StockChatroom.Worker.Configuration;

public static class ApplicationConfiguration
{
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddSignalR();
        _ = services.AddSingleton<SignalRHub>();

        var stooqAppSettings = services.AddAppSettings<StooqAppSettings>(configuration, nameof(StooqAppSettings));
        _ = services.AddHttpClient<IStooqService, StooqService>(c => c.BaseAddress = new Uri(stooqAppSettings.BaseUrl));

        services.InjectEvents();
        services.InjectUseCases();
    }

    private static void InjectUseCases(this IServiceCollection services)
    {
        // Messages
        services.AddUseCase<ProccessCommandInput, ProccessCommandOutput, ProccessCommandUseCase>();
        services.AddUseCase<NotifyMessageInput, NotifyMessageOutput, NotifyMessageUseCase>();

        // ChatRoom
        services.AddUseCase<NotifyChatRoomCreationInput, NotifyChatRoomCreationOutput, NotifyChatRoomCreationUseCase>();
    }

    private static void InjectEvents(this IServiceCollection services)
    {
        // Messages
        services.AddEvent<CommandSentEvent, EventProcessing.EventHandler>();
        services.AddEvent<MessageSentEvent, EventProcessing.EventHandler>();
        services.AddEvent<ChatRoomCreatedEvent, EventProcessing.EventHandler>();
    }

    /// <summary>
    /// Adds a command and the handler that will manage it.
    /// </summary>
    /// <typeparam name="TInput">The input to be handled.</typeparam>
    /// <typeparam name="TOutput">The expected response after handling the input.</typeparam>
    /// <typeparam name="TCommandHandler">The <see cref="CommandHandlerBase"/> that will handle the command.</typeparam>
    /// <param name="services">The collection of services where this command will be available.</param>
    private static void AddUseCase<TInput, TOutput, TCommandHandler>(this IServiceCollection services)
        where TInput : IRequest<TOutput>
        where TCommandHandler : class, IRequestHandler<TInput, TOutput> =>
        _ = services.AddSingleton<IRequestHandler<TInput, TOutput>, TCommandHandler>();

    /// <summary>
    /// Adds a event and the handler that will manage it.
    /// </summary>
    /// <typeparam name="TEvent">The event to be handled.</typeparam>
    /// <typeparam name="TEventHandler">The <see cref="EventHandlerBase"/> that will handle the event.</typeparam>
    /// <param name="services">The collection of services where this event will be available.</param>
    private static void AddEvent<TEvent, TEventHandler>(this IServiceCollection services)
        where TEvent : EventBase
        where TEventHandler : class, INotificationHandler<TEvent> =>
        _ = services.AddScoped<INotificationHandler<TEvent>, TEventHandler>();
}
