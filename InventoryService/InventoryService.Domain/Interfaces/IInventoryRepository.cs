using InventoryService.Domain.Entities;

namespace InventoryService.Domain.Interfaces
{
    // Handles data access operations for the Stock entity
    // Stock varlığı için veri erişim operasyonlarını yönetir
    public interface IInventoryRepository
    {
        // Retrieves the stock information based on the given product ID
        // Verilen ürün ID'sine göre stok bilgisini getirir
        Task<Stock?> GetByProductIdAsync(Guid productId);
    }
}
