using System;

namespace StockChatroom.Application.Configuration.AppSettings;

public class AppSettingsException : Exception
{
    private const string MessageTemplate = "The {0} appsettings.json section could not be found or is invalid.";

    public AppSettingsException(string appSettingsSectionName)
        : base(string.Format(MessageTemplate, appSettingsSectionName))
    {
    }
}
