namespace OrderService.Domain.Entities
{
    public enum OrderStatus
    {
        Pending = 1, // Sipariş ilk oluşturuğunda (Stok kontrolü bekleniyor)
        Confirmed = 2, // Syok onaylandı
        StockRejected = 3, // Syok yetersizliği nedeniyle reddedildi
    }
}
