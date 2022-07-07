namespace StockChatroom.Server.Configuration.AutoMapper;

public static class AutoMapperConfiguration
{
    public static void ConfigureAutoMapper(this IServiceCollection services) =>
        _ = services.AddAutoMapper(typeof(AutoMapperProfile));
}
