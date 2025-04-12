using Project.DTO;
using Project.Repositories;

namespace Project.Contract
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        
    }
}
