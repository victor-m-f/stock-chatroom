using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace StockChatroom.Application.Services.RabbitMq;

public class MessageProducer : IMessageProducer
{
    private readonly ILogger<MessageProducer> _logger;

    public MessageProducer(ILogger<MessageProducer> logger)
    {
        _logger = logger;
    }

    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "stock", type: ExchangeType.Fanout);

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(
            exchange: "stock",
            routingKey: "commands",
            basicProperties: null,
            body: body);

        _logger.LogInformation("{@mesage} sent to {@path}.", message, "stock/commands");
    }
}
