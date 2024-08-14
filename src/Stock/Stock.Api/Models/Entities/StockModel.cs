using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Api.Models.Entities
{
    public class StockModel
    {
        [Column("StockId")]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TotalCount { get; set; }
        public int ReservedCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
