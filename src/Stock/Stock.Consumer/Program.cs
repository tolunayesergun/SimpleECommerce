using Core.Data.Abstracts;
using Core.Data;
using Core.Models.Constants;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Stock.Consumer;
using Stock.Consumer.Consumers;
using Stock.Consumer.Data;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<StockDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("StockDBConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbContext, StockDbContext>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConsumeOrderCreated>();
    x.AddConsumer<ConsumeOrderConfirmed>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(RabbitMqConstants.Host, "/", c =>
        {
            c.Username(RabbitMqConstants.Username);
            c.Password(RabbitMqConstants.Password);
        });

        cfg.ReceiveEndpoint(RabbitMqConstants.OrderCreated, e =>
        {
            e.ConfigureConsumer<ConsumeOrderCreated>(ctx);
            e.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(5)));
        });

        cfg.ReceiveEndpoint(RabbitMqConstants.OrderConfirmed, e =>
        {
            e.ConfigureConsumer<ConsumeOrderConfirmed>(ctx);
            e.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(5)));
        });
    });
});


var host = builder.Build();
host.Run();
