namespace OrderService.Domain.Entities
{
    // set işlemleri private olarak tanımlandı çünkü:
    // Sonradan dışarıdan birinin fiyatı veya adeti kafasına göre değiştirmesini engellemek isityoruz.
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        // Sadece bu sınıfın veya Order'ın içinden oluşturulabilmesi için
        internal OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}