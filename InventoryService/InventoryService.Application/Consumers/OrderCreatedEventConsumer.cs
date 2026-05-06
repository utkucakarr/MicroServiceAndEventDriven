using InventoryService.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Application.Events;

namespace InventoryService.Application.Consumers
{
    // Listens to the RabbitMQ queue for OrderCreatedEvent messages
    // OrderCreatedEvent mesajları için RabbitMQ kuyruğunu dinler
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IInventoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderCreatedEventConsumer> _logger;


        public OrderCreatedEventConsumer(
        IInventoryRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<OrderCreatedEventConsumer> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // This method is triggered automatically when a message arrives
        // Bir mesaj geldiğinde bu metot otomatik olarak tetiklenir
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation($"OrderCreatedEvent received! Order ID: {message.OrderId}");
            // Türkçe Log: "OrderCreatedEvent alındı! Sipariş ID: {message.OrderId}"

            foreach (var item in message.Items)
            {
                // 1. Get the stock from the database
                // 1. Veritabanından stoğu getir
                var stock = await _repository.GetByProductIdAsync(item.ProductId);

                if (stock != null)
                {
                    // 2. Decrease the stock using our Domain business rule
                    // 2. Domain iş kuralımızı kullanarak stoğu düşür
                    try
                    {
                        stock.Decrease(item.Quantity);
                        _logger.LogInformation($"Stock decreased for Product: {item.ProductId}. Remaining: {stock.Quantity}");
                        // Türkçe Log: "Ürün için stok düşüldü: {item.ProductId}. Kalan: {stock.Quantity}"
                    }
                    catch (Exception ex)
                    {
                        // If stock is insufficient, it throws an exception from the Domain entity
                        // Eğer stok yetersizse, Domain entity'sinden fırlatılan hatayı yakalar
                        _logger.LogError($"Error decreasing stock for Product: {item.ProductId}. Reason: {ex.Message}");
                        // Türkçe Log: "Ürün için stok düşülürken hata oluştu: {item.ProductId}. Neden: {ex.Message}"

                        // Not: Gerçek hayatta burada "StockRejectedEvent" fırlatıp siparişi iptal edebiliriz!
                    }
                }
                else
                {
                    _logger.LogWarning($"Stock not found for Product: {item.ProductId}");
                    // Türkçe Log: "Ürün için stok bulunamadı: {item.ProductId}"
                }
            }

            // 3. Save all changes to the database using Unit Of Work
            // 3. Unit Of Work kullanarak tüm değişiklikleri veritabanına kaydet
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
