using Core.Data.Abstracts;
using Microsoft.EntityFrameworkCore;
using Stock.Consumer.Models.Entities;
namespace Stock.Consumer.Data
{
    public class StockDbContext : DbContext, IDbContext
    {
        public StockDbContext(DbContextOptions options) : base(options) { }

        public DbSet<StockModel> Stocks { get; set; }

    }
}
