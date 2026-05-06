namespace InventoryService.Domain.Entities
{
    public class Stock
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; } // Sipariş servisinden gelen ProductId ile eşleşecek
        public int Quantity { get; private set; }

        // Entity Framework'ün arka planda çalışabilmesi için boş bir constructor bırakıyoruz
        private Stock() { }

        public Stock(Guid productId, int initialQuantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = initialQuantity;
        }

        // İş Kuralı (Business Rule): Stok düşme işlemi sadece bu metotla yapılabilir
        public void Decrease(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Düşülecek miktar 0'dan büyük olmalıdır.");

            if (Quantity < amount)
                throw new InvalidOperationException($"Yetersiz stok! Ürün ID: {ProductId}");

            Quantity -= amount;
        }
    }
}