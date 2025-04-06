using Project.Contract;
using Project.DTO;

namespace Project.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _IProductRepository;
        public ProductService(IProductRepository productRepository)
        {
            _IProductRepository = productRepository;
            
        }
        public async Task AddProductAsync(ProductCreateDTO dto) 
        {
            var product = new Product
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                BrandId = dto.BrandId,
                AdminId = dto.AdminId,

            };
            //all logic are here 
           await _IProductRepository.addProductAsync(product);
        }

    }
}
