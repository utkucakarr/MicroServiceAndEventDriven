using MassTransit;
using MediatR;
using OrderService.Application.Commands;
using OrderService.Application.Events;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint; // <-- MESAJ FIRLATICI (Postacı) EKLENDİ

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. Domain nesnesini oluştur (Daha önce yazdığımız Factory method ile)
            var order = Order.Create(request.CustomerId);

            // 2. Sipariş kalemlerini ekle
            foreach (var item in request.Items)
            {
                order.AddOrderItem(item.ProductId, item.Quantity, item.UnitPrice);
            }

            // 3. Veritabanına kaydetmek için Repository'e gönder
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            // 3. İŞTE EKSİK OLAN SİHİRLİ KISIM: SİPARİŞ OLUŞTU MESAJINI RABBITMQ'YA FIRLAT!
            var orderCreatedEvent = new OrderCreatedEvent(order.Id, order.CustomerId, request.Items);

            await _publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
            // Türkçe: "Hey RabbitMQ, bu olayı (event) al ve ilgilenen herkese (Inventory Service) duyur!"

            return order.Id; // API'ye oluşan siparişin ID'sini dönüyoruz
        }
    }
}
