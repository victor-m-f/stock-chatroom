using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StockChatroom.Application.Configuration.AppSettings;

/// <summary>
/// Exposes methods for configuring AppSettings.
/// </summary>
public static class AppSettingsConfiguration
{
    /// <summary>
    /// Registers a configuration instance which TOptions will bind against.
    /// </summary>
    /// <typeparam name="TAppSettings">App Settings type to register.</typeparam>
    /// <param name="services">The collection of services the AppSettings will be available in.</param>
    /// <param name="configuration">The set of key/value application configuration properties.</param>
    /// <param name="sectionName">The name of the section in appsettings.json file that will be used to configure the AppSettings.</param>
    /// <returns>An instance of the already configured AppSettings.</returns>
    public static TAppSettings AddAppSettings<TAppSettings>(this IServiceCollection services, IConfiguration configuration, string sectionName)
        where TAppSettings : AppSettingsBase
    {
        var appSettingsSection = configuration.GetSection(sectionName);
        _ = services.Configure<TAppSettings>(appSettingsSection);

        var appSettings = appSettingsSection.Get<TAppSettings>();

        return appSettings?.IsValid == true ? appSettings : throw new AppSettingsException(sectionName);
    }
}
