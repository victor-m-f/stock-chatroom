using Microsoft.AspNetCore.Mvc;

namespace StockChatroom.Server.Configuration;

public static class VersioningConfiguration
{
    public static void ConfigureVersioning(this IServiceCollection services, int majorVersion, int minorVersion)
    {
        _ = services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
            options.ReportApiVersions = true;
        });

        _ = services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}
