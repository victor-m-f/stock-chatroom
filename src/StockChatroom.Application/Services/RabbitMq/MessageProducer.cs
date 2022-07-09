using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace StockChatroom.Application.Services.RabbitMq;

public class MessageProducer : IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        _ = channel.QueueDeclare("commands");

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: string.Empty, routingKey: "commands", body: body);
    }
}
