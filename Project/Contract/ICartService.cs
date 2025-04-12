using Project.Models;

namespace Project.Contract
{
    public interface ICartService
    {
        Task<Cart?> GetCartByIdAsync(int id);
        Task<Cart?> GetCartByCustomerIdAsync(int customerId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int id);
    }
} 