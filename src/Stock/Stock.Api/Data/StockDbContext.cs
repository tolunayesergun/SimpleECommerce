using Core.Data.Abstracts;
using Microsoft.EntityFrameworkCore;
using Stock.Api.Models.Entities;

namespace Stock.Api.Data
{
    public class StockDbContext : DbContext, IDbContext
    {
        public StockDbContext(DbContextOptions options) : base(options) { }

        public DbSet<StockModel> Stocks { get; set; }

    }
}