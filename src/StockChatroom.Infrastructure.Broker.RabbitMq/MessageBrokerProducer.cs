using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StockChatroom.Domain.Services;
using StockChatroom.Infrastructure.Broker.RabbitMq.Configuration;
using System.Text;
using System.Text.Json;

namespace StockChatroom.Infrastructure.Broker.RabbitMq;

internal class MessageBrokerProducer : IMessageBrokerProducer
{
    private readonly ILogger<MessageBrokerProducer> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBrokerProducer(ILogger<MessageBrokerProducer> logger, IOptions<RabbitMqAppSettings> rabbitMqAppSettingsOptions)
    {
        _logger = logger;

        var factory = new ConnectionFactory()
        {
            HostName = rabbitMqAppSettingsOptions.Value.Host,
            Port = rabbitMqAppSettingsOptions.Value.Port,
        };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            _logger.LogInformation("Connected to RabbitMQ {@host}", rabbitMqAppSettingsOptions.Value.Host);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not connect to the RabbitMQ", ex);
            throw;
        }
    }

    public void PublishEvent(object @event)
    {
        var message = JsonSerializer.Serialize(@event);

        if (_connection.IsOpen)
        {
            _logger.LogInformation("RabbitMQ Connection Open, sending message {@message}", @event);
            SendMessage(message);
        }
        else
        {
            _logger.LogInformation("RabbitMQ connection closed, not sending message");
        }
    }

    public void Dispose()
    {
        _logger.LogInformation("MessageBus Disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: "trigger",
            routingKey: string.Empty,
            basicProperties: null,
            body: body);
        _logger.LogInformation("{@message} sent", message);
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e) =>
        _logger.LogInformation("RabbitMQ Connection Shutdown");
}
