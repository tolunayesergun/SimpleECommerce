using Core.Models.Events.Stock;
using MassTransit;

namespace Order.Consumer.Consumers
{
    public class ConsumeStockReserved : IConsumer<StockReserved>
    {
        private readonly IPublishEndpoint _publish;
        private readonly ILogger<ConsumeStockReserved> _logger;

        public ConsumeStockReserved(ILogger<ConsumeStockReserved> logger, IPublishEndpoint publish)
        {
            _logger = logger;
            _publish = publish;
        }

        public async Task Consume(ConsumeContext<StockReserved> context)
        {
            //Stock reservelendi order confirmlenebilir.
            var deneme = context;
        }
    }
}
