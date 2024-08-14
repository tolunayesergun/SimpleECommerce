namespace Core.Models.Product
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public bool ProductReserved { get; set; }

        public int Count { get; set; }
    }
}
