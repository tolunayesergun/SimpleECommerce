using Core.Models.Product;

namespace Order.Api.Models.DTOs
{
    public class OrderDetailDTO
    {
        public List<ProductDetailDTO> productDetails { get; set; }
        public int CustomerId { get; set; }
    }

    public class ProductDetailDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
