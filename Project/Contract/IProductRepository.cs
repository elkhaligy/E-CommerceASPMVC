using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Models;
using Project.Repositories;

namespace Project.Contract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>?> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>?> GetByBrandIdAsync(int brandId);
        Task<IEnumerable<Product>?> GetByAdminIdAsync(int adminId);
        Task<IEnumerable<Product>?> SearchByNameAsync(string name);
        Task<IEnumerable<Product>?> FilterByCategoryAndBrandAsync(int? categoryId, int? brandId);
        Task<PagedResult<Product>> GetPagedProductsAsync(int pageNumber, int pageSize);

    }
}
