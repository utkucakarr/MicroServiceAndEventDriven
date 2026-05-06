namespace OrderService.Application.Commands;

// 1. Record Tanımı: OrderItemDto
public record OrderItemDto(Guid ProductId, int Quantity, decimal UnitPrice);