using MediatR;

namespace OrderService.Application.Commands;

// 2. Record Tanımı: CreateOrderCommand, IRequest ile mediator interfacini uyguladık
public record CreateOrderCommand(Guid CustomerId, List<OrderItemDto> Items) : IRequest<Guid>;
