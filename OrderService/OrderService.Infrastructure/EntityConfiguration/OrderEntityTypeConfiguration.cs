using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.EntityConfiguration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            // Müşteri Id'si zorunlu
            builder.Property(o => o.CustomerId).IsRequired();

            // Enum'ı veritabanına integer (sayı) yerine string (yazı) olarak kaydetmek genelde daha güvenlidir
            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .IsRequired();

            // Order -> OrderItems ilişkisi
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey("OrderId");
        }
    }
}
