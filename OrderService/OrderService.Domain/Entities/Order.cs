namespace OrderService.Domain.Entities
{
    // Sipariş kalemlerinin yönetmek ve toplam tutarı hesaplamak tamamen bu sınıfın sorumluluğu (SRP). Dışarıdan doprudan listeye eleman eklenemez, Add OrderItem metodu kullanılmak zorundadır.
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public OrderStatus Status { get; private set; }

        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public decimal TotalPrice => _orderItems.Sum(item => item.UnitPrice * item.Quantity);

        private Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CreatedDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }
        

        // Factory Method Pattern: Nesne üretimini kontrollü bir şekilde yapıyoruz
        public static Order Create(Guid customerId)
        {
            return new Order(customerId);
        }

        public void AddOrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Adet 0'dan büyük olmalıdır.");

            var orderItem = new OrderItem(productId, quantity, unitPrice);
            _orderItems.Add(orderItem);
        }

        // RabbitMQ'dan dönüş geldiğinde bu metodu çağıracağız
        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
