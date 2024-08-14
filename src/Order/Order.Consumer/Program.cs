using Core.Models.Constants;
using MassTransit;
using Order.Consumer;
using Order.Consumer.Consumers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConsumeStockReserved>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(RabbitMqConstants.Host, "/", c =>
        {
            c.Username(RabbitMqConstants.Username);
            c.Password(RabbitMqConstants.Password);
        });

        cfg.ReceiveEndpoint(RabbitMqConstants.StockReserved, e =>
        {
            e.ConfigureConsumer<ConsumeStockReserved>(ctx);
            e.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(5)));
        });
    });
});


var host = builder.Build();
host.Run();
