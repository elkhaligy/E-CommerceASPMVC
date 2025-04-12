using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _context;

        public CartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CartId == id);
        }

        public async Task<Cart?> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<Cart> CreateAsync(Cart cart)
        {
            cart.CreatedAt = DateTime.UtcNow;
            cart.UpdatedAt = DateTime.UtcNow;
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> UpdateAsync(Cart cart)
        {
            cart.UpdatedAt = DateTime.UtcNow;
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task DeleteAsync(int id)
        {
            var cart = await GetByIdAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
} 