using Project.Contract;
using Project.DTO;
using Project.Models;

namespace Project.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductService(
            IProductRepository productRepository,
            ICategoryService categoryService,
            IBrandService brandService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>?> GetProductsByCategoryAsync(int categoryId)
        {
            // Business logic: Verify category exists
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found");

            return await _productRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>?> GetProductsByBrandAsync(int brandId)
        {
            // Business logic: Verify brand exists
            var brand = await _brandService.GetBrandByIdAsync(brandId);
            if (brand == null)
                throw new KeyNotFoundException($"Brand with ID {brandId} not found");

            return await _productRepository.GetByBrandIdAsync(brandId);
        }

        public async Task<IEnumerable<Product>?> GetProductsByAdminAsync(int adminId)
        {
            return await _productRepository.GetByAdminIdAsync(adminId);
        }

        public async Task<IEnumerable<Product>?> SearchProductsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Search term cannot be empty", nameof(name));

            return await _productRepository.SearchByNameAsync(name);
        }

        public async Task AddProductAsync(Product product)
        {
            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {product.CategoryId} not found");

            var brand = await _brandService.GetBrandByIdAsync(product.BrandId);
            if (brand == null)
                throw new KeyNotFoundException($"Brand with ID {product.BrandId} not found");

            var existingProduct = (await _productRepository.GetAllAsync())
                .FirstOrDefault(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
            if (existingProduct != null)
                throw new InvalidOperationException($"A product with name '{product.Name}' already exists");

            _productRepository.Add(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {product.ProductId} not found");

            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {product.CategoryId} not found");

            // Business logic: Check if brand exists
            var brand = await _brandService.GetBrandByIdAsync(product.BrandId);
            if (brand == null)
                throw new KeyNotFoundException($"Brand with ID {product.BrandId} not found");

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            // Business logic: Check if product exists
            var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {product.ProductId} not found");

            // Business logic: Check if product can be deleted (e.g., no active orders)
            if (existingProduct.Items?.Any() == true)
                throw new InvalidOperationException("Cannot delete product with existing orders");

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

    }
}
