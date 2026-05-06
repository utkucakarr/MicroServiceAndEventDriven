using OrderService.Application.Commands;

namespace OrderService.Application.Events
{
    // Represents the event published when an order is successfully created
    // Bir sipariş başarıyla oluşturulduğunda RabbitMQ'ya fırlatılacak olan mesaj sözleşmesi
    public record OrderCreatedEvent(
        Guid OrderId,
        Guid CustomerId,
        List<OrderItemDto> Items
    );
}
