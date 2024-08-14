using Core.Data.Abstracts;
using Microsoft.EntityFrameworkCore;
using Order.Api.Models.Entities;

namespace Order.Api.Data
{
    public class OrderDbContext : DbContext, IDbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options) { }

        public DbSet<OrderModel> Orders { get; set; }
        
    }
}
