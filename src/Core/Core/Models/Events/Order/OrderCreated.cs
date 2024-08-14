using Core.Models.Product;

namespace Core.Models.Events.Order
{
    public class OrderCreated
    {
        public int OrderId { get; set; }
        public required List<ProductDetail> Products { get; set; }
    }
}
