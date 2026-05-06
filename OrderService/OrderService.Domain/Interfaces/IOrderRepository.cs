using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task SaveChangesAsync();
    }
}
