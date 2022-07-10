using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockChatroom.Application.Configuration.AppSettings;
using StockChatroom.Application.Services.EventProcessing;
using StockChatroom.Domain.Services;

namespace StockChatroom.Infrastructure.Broker.RabbitMq.Configuration;

public static class RabbitMqConfiguration
{
    public static void AddRabbitMqProducer(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddAppSettings<RabbitMqAppSettings>(configuration, nameof(RabbitMqAppSettings));
        _ = services.AddSingleton<IMessageBrokerProducer, MessageBrokerProducer>();
    }

    public static void AddRabbitMqConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddAppSettings<RabbitMqAppSettings>(configuration, nameof(RabbitMqAppSettings));
        _ = services.AddHostedService<MessageBusSubscriber>();
        _ = services.AddSingleton<IEventProcessor, EventProcessor>();
    }
}
