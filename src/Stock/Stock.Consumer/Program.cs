using Core.Models.Constants;
using MassTransit;
using Stock.Consumer;
using Stock.Consumer.Consumers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConsumeOrderCreated>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", c =>
        {
            c.Username(RabbitMqConstants.Username);
            c.Password(RabbitMqConstants.Password);
        });

        cfg.ReceiveEndpoint(RabbitMqConstants.OrderCreated, e =>
        {
            e.ConfigureConsumer<ConsumeOrderCreated>(ctx);
            e.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(5)));
        });
    });
});


var host = builder.Build();
host.Run();
