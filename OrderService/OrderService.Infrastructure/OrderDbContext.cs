using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API yapılandırmalarını ayrı dosyalardan okumasını söylüyoruz
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
