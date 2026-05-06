using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Data
{
    // Configures the Stock entity and adds initial seed data
    // Stock varlığını yapılandırır ve başlangıç verilerini ekler
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            // Table name configuration
            // Tablo adı yapılandırması
            builder.ToTable("Stocks");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.ProductId).IsRequired();
            builder.Property(s => s.Quantity).IsRequired();

            // Seed Data: Adding dummy stocks for testing purposes
            // Başlangıç Verisi: Test amaçlı sahte stoklar ekliyoruz
            // Not: Order servisini test ederken kullandığımız Product ID'leri kullanıyoruz!
            var productId1 = Guid.Parse("22222222-3333-4444-5555-666666666666");
            var productId2 = Guid.Parse("11111111-2222-3333-4444-555555555555");

            builder.HasData(
                            new
                            {
                                Id = Guid.Parse("12345632-2222-3333-4444-555555555555"), // Sabit Id
                                ProductId = productId1,
                                Quantity = 100
                            },
                            new
                            {
                                Id = Guid.Parse("12345623-3333-4444-5555-666666666666"), // Sabit Id
                                ProductId = productId2,
                                Quantity = 50
                            }
                        );
        }
    }
}
