using MediatR;
using StockChatroom.Application.Configuration.AppSettings;
using StockChatroom.Application.Services.Hubs;
using StockChatroom.Application.Services.RabbitMq;
using StockChatroom.Application.Services.Stooq;
using StockChatroom.Application.UseCases.Messages.ProccessCommand;

namespace StockChatroom.Worker.Configuration;

public static class ApplicationConfiguration
{
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddScoped<IMessageProducer, MessageProducer>();

        var stooqAppSettings = services.AddAppSettings<StooqAppSettings>(configuration, nameof(StooqAppSettings));
        _ = services.AddHttpClient<IStooqService, StooqService>(c => c.BaseAddress = new Uri(stooqAppSettings.BaseUrl));

        services.InjectUseCases();
    }

    private static void InjectUseCases(this IServiceCollection services)
    {
        // Messages
        services.AddUseCase<ProccessCommandInput, ProccessCommandOutput, ProccessCommandUseCase>();
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
        _ = services.AddScoped<IRequestHandler<TInput, TOutput>, TCommandHandler>();
}
