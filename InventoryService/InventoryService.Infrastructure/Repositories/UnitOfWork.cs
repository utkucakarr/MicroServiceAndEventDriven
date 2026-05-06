using InventoryService.Domain.Interfaces;
using InventoryService.Infrastructure.Data;

namespace InventoryService.Infrastructure.Repositories
{
    // Concrete implementation of IUnitOfWork to handle transactions
    // Transaction (işlem) yönetimi için IUnitOfWork'ün somut uyarlaması
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;

        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
        }

        // Saves all changes made in the context to the database
        // Context üzerinde yapılan tüm değişiklikleri veritabanına kaydeder
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
