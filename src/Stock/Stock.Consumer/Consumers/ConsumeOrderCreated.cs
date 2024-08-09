using Core.Models.Events;
using MassTransit;

namespace Stock.Consumer.Consumers
{
    public class ConsumeOrderCreated : IConsumer<CreatedOrderModel>
    {
        public Task Consume(ConsumeContext<CreatedOrderModel> context)
        {

            var deneme = context;
            throw new ArgumentNullException();
            return Task.CompletedTask;
        }
    }
}
