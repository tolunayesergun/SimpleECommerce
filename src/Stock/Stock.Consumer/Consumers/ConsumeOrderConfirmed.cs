using Core.Models.Events.Order;
using MassTransit;

namespace Stock.Consumer.Consumers
{
    public class ConsumeOrderConfirmed : IConsumer<OrderConfirmed>
    {
        private readonly IPublishEndpoint _publish;
        private readonly ILogger<ConsumeOrderCreated> _logger;

        public ConsumeOrderConfirmed(ILogger<ConsumeOrderCreated> logger, IPublishEndpoint publish)
        {
            _logger = logger;
            _publish = publish;
        }

        public async Task Consume(ConsumeContext<OrderConfirmed> context)
        {
            //confirmlenen order için stokları düşücek
        }
    }

}
