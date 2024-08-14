namespace Core.Models.Events.Stock
{
    public class StockReserved
    {
        public int StockId { get; set; }
        public int OrderId { get; set;}
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
