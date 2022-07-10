using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockChatroom.Domain.Services;
using StockChatroom.Infrastructure.Broker.RabbitMq.Configuration;
using System.Text;

namespace StockChatroom.Infrastructure.Broker.RabbitMq;

public class MessageBusSubscriber : BackgroundService
{
    private readonly ILogger<MessageBusSubscriber> _logger;
    private readonly RabbitMqAppSettings _rabbitMqAppSettings;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageBusSubscriber(
        ILogger<MessageBusSubscriber> logger,
        IOptions<RabbitMqAppSettings> rabbitMqAppSettingsOptions,
        IEventProcessor eventProcessor)
    {
        _logger = logger;
        _rabbitMqAppSettings = rabbitMqAppSettingsOptions.Value;
        _eventProcessor = eventProcessor;

        InitializeRabbitMQ();
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }

        base.Dispose();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (moduleHandle, ea) =>
        {
            _logger.LogInformation("Event Received");
            var body = ea.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            await _eventProcessor.ProcessEvent(notificationMessage, stoppingToken);
        };

        _ = _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory() { HostName = _rabbitMqAppSettings.Host, Port = _rabbitMqAppSettings.Port };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(
            queue: _queueName,
            exchange: "trigger",
            routingKey: string.Empty);

        _logger.LogInformation("Listening to RabbitMq {@host}:{@port}", _rabbitMqAppSettings.Host, _rabbitMqAppSettings.Port);

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e) => _logger.LogInformation("RabbitMq connection shutdown");
}
