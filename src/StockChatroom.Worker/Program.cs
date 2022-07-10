using MediatR;
using StockChatroom.Application.Services.Hubs;
using StockChatroom.Infrastructure.Broker.RabbitMq.Configuration;
using StockChatroom.Worker.Configuration;
using StockChatroom.Worker.Configuration.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAutoMapper();
_ = builder.Services.AddMediatR(typeof(Program));

builder.Services.AddCors(x => x.AddDefaultPolicy(builder =>
{
    _ = builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddRabbitMqProducer(builder.Configuration);
builder.Services.AddRabbitMqConsumer(builder.Configuration);
builder.Services.ConfigureApplication(builder.Configuration);

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();

app.MapHub<SignalRHub>("/signalRHub");

app.Run();