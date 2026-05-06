namespace InventoryService.Domain.Interfaces
{
    // Handles database transaction operations
    // Veritabanı transaction (işlem) operasyonlarını yönetir
    public interface IUnitOfWork
    {
        // Commits the changes to the database
        // Değişiklikleri veritabanına kaydeder
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
