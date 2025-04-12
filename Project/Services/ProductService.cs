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

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            // Business logic: Verify category exists
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found");

            return await _productRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId)
        {
            // Business logic: Verify brand exists
            var brand = await _brandService.GetBrandByIdAsync(brandId);
            if (brand == null)
                throw new KeyNotFoundException($"Brand with ID {brandId} not found");

            return await _productRepository.GetByBrandIdAsync(brandId);
        }

        public async Task<IEnumerable<Product>> GetProductsByAdminAsync(int adminId)
        {
            return await _productRepository.GetByAdminIdAsync(adminId);
        }

        public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Search term cannot be empty", nameof(name));

            return await _productRepository.SearchByNameAsync(name);
        }

        public async Task AddProductAsync(Product product)
        {
            // Business logic: Validate product data
            // ValidateProduct(product);

            // Business logic: Check if category exists
            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {product.CategoryId} not found");

            // Business logic: Check if brand exists
            var brand = await _brandService.GetBrandByIdAsync(product.BrandId);
            if (brand == null)
                throw new KeyNotFoundException($"Brand with ID {product.BrandId} not found");

            // Business logic: Check for duplicate product names
            var existingProduct = (await _productRepository.SearchByNameAsync(product.Name))
                .FirstOrDefault(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
            if (existingProduct != null)
                throw new InvalidOperationException($"A product with name '{product.Name}' already exists");

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            // Business logic: Validate product exists
            var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {product.ProductId} not found");

            // Business logic: Validate product data
            ValidateProduct(product);

            // Business logic: Check if category exists
            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {product.CategoryId} not found");

            // Business logic: Check if brand exists
            var brand = await _brandService.GetBrandByIdAsync(product.BrandId);
            if (brand == null)
                throw new KeyNotFoundException($"Brand with ID {product.BrandId} not found");

            await _productRepository.UpdateAsync(product);
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

            await _productRepository.DeleteAsync(product);
        }

        // private void ValidateProduct(Product product)
        // {
        //     if (product == null)
        //         throw new ArgumentNullException(nameof(product));

        //     if (string.IsNullOrWhiteSpace(product.Name))
        //         throw new ArgumentException("Product name cannot be empty", nameof(product.Name));

        //     if (product.Price <= 0)
        //         throw new ArgumentException("Product price must be greater than zero", nameof(product.Price));

        //     if (product.CategoryId <= 0)
        //         throw new ArgumentException("Invalid category ID", nameof(product.CategoryId));

        //     if (product.BrandId <= 0)
        //         throw new ArgumentException("Invalid brand ID", nameof(product.BrandId));

        //     if (product.AdminId <= 0)
        //         throw new ArgumentException("Invalid admin ID", nameof(product.AdminId));
        // }
    }
}
