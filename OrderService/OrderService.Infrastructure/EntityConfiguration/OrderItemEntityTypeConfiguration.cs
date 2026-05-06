using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.EntityConfiguration
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.ProductId).IsRequired();
            builder.Property(oi => oi.Quantity).IsRequired();

            // Fiyat (para) tuttuğumuz için virgülden sonraki hassasiyeti (precision) ayarlıyoruz
            builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
