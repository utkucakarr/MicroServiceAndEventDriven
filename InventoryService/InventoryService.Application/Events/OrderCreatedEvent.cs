// We use the exact same namespace as the Order Service
// Sipariş Servisi ile tamamen aynı namespace'i kullanıyoruz
namespace OrderService.Application.Events
{
    // Represents the event published when an order is successfully created
    // Bir sipariş başarıyla oluşturulduğunda fırlatılan olayı temsil eder
    public record OrderCreatedEvent(
        Guid OrderId,
        Guid CustomerId,
        List<OrderItemDto> Items
    );
}
