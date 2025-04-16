using Project.Contract;
using Project.Models;

namespace Project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task AddCategoryAsync(Category category)
        {
            //ValidateCategory(category);

            // Check for duplicate category name
            var existingCategory = (await _categoryRepository.GetAllAsync())
                .FirstOrDefault(c => c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase));
            
            if (existingCategory != null)
            {
                throw new InvalidOperationException($"A category with name '{category.Name}' already exists");
            }

            _categoryRepository.Add(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            //ValidateCategory(category);

            // Check if category exists
            var existingCategory = await _categoryRepository.GetByIdAsync(category.CategoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {category.CategoryId} not found");
            }

            // Check for duplicate category name (excluding current category)
            var duplicateCategory = (await _categoryRepository.GetAllAsync())
                .FirstOrDefault(c => c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase) 
                    && c.CategoryId != category.CategoryId);
            
            if (duplicateCategory != null)
            {
                throw new InvalidOperationException($"A category with name '{category.Name}' already exists");
            }

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            // Check if category has products
            if (await CategoryHasProductsAsync(id))
            {
                throw new InvalidOperationException("Cannot delete category that has products");
            }

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id) != null;
        }

        public async Task<bool> CategoryHasProductsAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category?.Products?.Any() == true;
        }

        //private void ValidateCategory(Category category)
        //{
        //    if (category == null)
        //    {
        //        throw new ArgumentNullException(nameof(category));
        //    }

        //    if (string.IsNullOrWhiteSpace(category.Name))
        //    {
        //        throw new ArgumentException("Category name cannot be empty", nameof(category.Name));
        //    }

        //    if (category.Name.Length < 2)
        //    {
        //        throw new ArgumentException("Category name must be at least 2 characters long", nameof(category.Name));
        //    }

        //    if (category.Name.Length > 50)
        //    {
        //        throw new ArgumentException("Category name cannot exceed 50 characters", nameof(category.Name));
        //    }
        //}
    }
}
