using MediatR;
using StockChatroom.Worker.Configuration;
using StockChatroom.Worker.Configuration.AutoMapper;
using StockChatroom.Worker.Consumers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigureAutoMapper();
        _ = services.AddMediatR(typeof(Program));

        services.ConfigureApplication(hostContext.Configuration);
        _ = services.AddHostedService<CommandsConsumer>();
    })
    .Build();

await host.RunAsync();
