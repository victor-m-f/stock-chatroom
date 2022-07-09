using Microsoft.EntityFrameworkCore;
using StockChatroom.Domain.Entities;
using StockChatroom.Infrastructure.Data;

namespace StockChatroom.Server.Configuration;

internal static class IdentityServerConfiguration
{
    public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _ = services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        _ = services.AddDatabaseDeveloperPageExceptionFilter();

        _ = services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        _ = services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
    }
}
