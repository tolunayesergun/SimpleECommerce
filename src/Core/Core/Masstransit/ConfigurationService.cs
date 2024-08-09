using Core.Models.Constants;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.Logging.OperationName;

namespace Core.Masstransit
{
    public static class ConfigurationService
    {
        //public static IServiceCollection AddRabbitMqServices(this IServiceCollection services)
        //{
        //    services.AddMassTransit(x =>
        //    {
        //        x.UsingRabbitMq((ctx, cfg) =>
        //        {
        //            cfg.Host("localhost", "/", c =>
        //            {
        //                c.Username(RabbitMqConstants.Username);
        //                c.Password(RabbitMqConstants.Password);
        //            });
        //            cfg.ConfigureEndpoints(ctx);

        //            cfg.ReceiveEndpoint(RabbitMqConstants.OrderCreated, e =>
        //            {
        //                e.ConfigureConsumer<OrderCreated>(context);
        //            });
        //        });
        //    });
        //    return services;
        //}
    }
}
