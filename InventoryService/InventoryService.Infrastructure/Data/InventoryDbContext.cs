using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Data
{
    // Represents the database context for the Inventory Service
    // Stok Servisi için veritabanı bağlamını temsil eder
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Applies configurations from the current assembly (Fluent API & Seed Data)
            // Mevcut assembly'deki yapılandırmaları uygular (Fluent API ve Başlangıç Verileri)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
