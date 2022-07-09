using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockChatroom.Shared.Dtos.Messages;
using System.Text;

namespace StockChatroom.Worker.Consumers;

public class CommandsConsumer : BackgroundService
{
    private readonly ILogger<CommandsConsumer> _logger;
    private readonly IMediator _mediator;

    public CommandsConsumer(
        ILogger<CommandsConsumer> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "stock", type: ExchangeType.Fanout);

        var queueName = channel.QueueDeclare(
            queue: "commands",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null).QueueName;

        channel.QueueBind(
            queue: queueName,
            exchange: "stock",
            routingKey: string.Empty);

        _logger.LogInformation("Waiting for commands to proccess.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var message = JsonConvert.DeserializeObject<MessageDto>(Encoding.UTF8.GetString(ea.Body.ToArray()));

            var output = _mediator.Send(message, stoppingToken);
        };

        _ = channel.BasicConsume(
            queue: queueName,
            autoAck: true,
            consumer: consumer);
    }
}
