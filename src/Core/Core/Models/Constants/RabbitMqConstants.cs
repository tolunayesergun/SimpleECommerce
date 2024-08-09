namespace Core.Models.Constants
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri = "http://localhost:15672";
        public const string Username = "guest";
        public const string Password = "guest";

        public const string OrderCreated = "order.created";
        public const string OrderCanceled = "order.canceled";
        public const string StockReserved = "stock.reserved";
        public const string StockCanceled = "stock.canceled";


        // OrderCreated
        // OrderCanceled
        // StockReserved
        // StockCanceled
    }
}
