using StockChatroom.Application.Configuration.AppSettings;

namespace StockChatroom.Infrastructure.Broker.RabbitMq.Configuration;

public class RabbitMqAppSettings : AppSettingsBase
{
    public string Host { get; set; }
    public int Port { get; set; }

    public override bool IsValid => !string.IsNullOrEmpty(Host) && Port != int.MinValue;
}
