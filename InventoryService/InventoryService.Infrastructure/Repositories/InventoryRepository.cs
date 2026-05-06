using InventoryService.Domain.Entities;
using InventoryService.Domain.Interfaces;
using InventoryService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Repositories
{
    // Concrete implementation of IInventoryRepository
    // IInventoryRepository'nin somut uyarlaması
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _context;
        public InventoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // Retrieves the stock asynchronously by ProductId
        // ProductId'ye göre stoğu asenkron olarak getirir
        public async Task<Stock?> GetByProductIdAsync(Guid productId)
        {
            return await _context.Stocks
            .FirstOrDefaultAsync(s => s.ProductId == productId);
        }
    }
}
