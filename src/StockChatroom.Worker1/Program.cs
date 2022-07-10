using MediatR;
using StockChatroom.Infrastructure.Broker.RabbitMq.Configuration;
using StockChatroom.Worker.Configuration;
using StockChatroom.Worker.Configuration.AutoMapper;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigureAutoMapper();
        _ = services.AddMediatR(typeof(Program));

        services.AddRabbitMqProducer(hostContext.Configuration);
        services.AddRabbitMqConsumer(hostContext.Configuration);
        services.ConfigureApplication(hostContext.Configuration);
    })
    .Build();

await host.RunAsync();
