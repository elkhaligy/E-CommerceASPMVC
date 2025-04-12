using Project.Models;

namespace Project.Contract
{
    public interface ICartRepository
    {
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart?> GetByCustomerIdAsync(int customerId);
        Task<Cart> CreateAsync(Cart cart);
        Task<Cart> UpdateAsync(Cart cart);
        Task DeleteAsync(int id);
    }
} 