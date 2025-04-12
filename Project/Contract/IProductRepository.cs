using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Contract
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>?> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>?> GetByBrandIdAsync(int brandId);
        Task<IEnumerable<Product>?> GetByAdminIdAsync(int adminId);
        Task<IEnumerable<Product>?> SearchByNameAsync(string name);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task SaveChangesAsync();
    }
}
