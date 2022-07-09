using MediatR;
using Microsoft.AspNetCore.Authentication;
using StockChatroom.Application.Services.Hubs;
using StockChatroom.Server.Configuration;
using StockChatroom.Server.Configuration.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddMediatR(typeof(Program));
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureIdentityServer(builder.Configuration);
builder.Services.ConfigureVersioning(1, 0);

builder.Services.ConfigureApplication();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers().RequireAuthorization();
app.MapFallbackToFile("index.html");
app.MapHub<SignalRHub>("/signalRHub");

app.Run();
