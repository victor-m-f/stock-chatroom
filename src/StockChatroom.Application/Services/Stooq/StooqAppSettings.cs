using StockChatroom.Application.Configuration.AppSettings;

namespace StockChatroom.Application.Services.Stooq;

public class StooqAppSettings : AppSettingsBase
{
    public string BaseUrl { get; set; }

    public override bool IsValid => !string.IsNullOrEmpty(BaseUrl);
}
