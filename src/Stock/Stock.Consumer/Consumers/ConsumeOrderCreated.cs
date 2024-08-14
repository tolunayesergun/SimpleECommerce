using Core.Data.Abstracts;
using Core.Models.Events.Order;
using Core.Models.Events.Stock;
using Core.Models.Product;
using MassTransit;
using Stock.Consumer.Models.Entities;
using System.Runtime.InteropServices;

namespace Stock.Consumer.Consumers
{
    public class ConsumeOrderCreated : IConsumer<OrderCreated>
    {
        private readonly IPublishEndpoint _publish;
        private readonly ILogger<ConsumeOrderCreated> _logger;
        private readonly IGenericRepository<StockModel> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ConsumeOrderCreated(ILogger<ConsumeOrderCreated> logger, IPublishEndpoint publish, IGenericRepository<StockModel> repository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _publish = publish;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            foreach (var product in context.Message.Products)
            {
                var stock = _repository.FindBy(x => x.ProductId == product.ProductId).FirstOrDefault();

                if (stock == null)
                {
                    var stockMessage = new StockReserved
                    {
                        OrderId = context.Message.OrderId,
                        Count = 0,
                        ProductId = product.ProductId,
                        StockId = -1
                    };

                    await _publish.Publish<StockReserved>(stockMessage);
                }
                else
                {
                    var unreservedStockCount = stock.TotalCount - stock.ReservedCount;
                    var orderReserveCount = 0;

                    if (unreservedStockCount >= product.Count)
                    {
                        orderReserveCount = product.Count;
                        stock.ReservedCount += product.Count;
                    }
                    else
                    {
                        orderReserveCount = unreservedStockCount;
                        stock.ReservedCount = stock.TotalCount;
                    }

                    stock.UpdatedDate = DateTime.Now;
                    _repository.Update(stock);
                    await _unitOfWork.CommitAsync();

                    var stockMessage = new StockReserved
                    {
                        OrderId = context.Message.OrderId,
                        Count = orderReserveCount,
                        ProductId = product.ProductId,
                        StockId = stock.Id
                    };

                    await _publish.Publish<StockReserved>(stockMessage);
                }
            }

        }
    }
}
