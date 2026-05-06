namespace OrderService.Application.Events
{
    // We use the exact same namespace as the Order Service to match the MassTransit message contract
    // MassTransit mesaj sözleşmesiyle eşleşmesi için Sipariş Servisi ile tamamen aynı namespace'i kullanıyoruz!
    public record OrderItemDto(Guid ProductId, int Quantity, decimal UnitPrice);
}
