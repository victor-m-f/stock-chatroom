using MediatR;
using Microsoft.AspNetCore.Authentication;
using StockChatroom.Application.Services.Hubs;
using StockChatroom.Infrastructure.Broker.RabbitMq.Configuration;
using StockChatroom.Server.Configuration;
using StockChatroom.Server.Configuration.AutoMapper;
using StockChatroom.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(Program));
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureIdentityServer(builder.Configuration);
builder.Services.ConfigureVersioning(1, 0);

builder.Services.AddRabbitMqProducer(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen();

var app = builder.Build();

_ = app.UseMiddleware<ErrorMiddleware>();

if (app.Environment.IsDevelopment())
{
    _ = app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}
else
{
    _ = app.UseExceptionHandler("/Error");
    _ = app.UseHsts();
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
