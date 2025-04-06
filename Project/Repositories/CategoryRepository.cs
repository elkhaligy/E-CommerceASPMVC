using Microsoft.EntityFrameworkCore;
using Project.Contract;

namespace Project.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationContext _context; 
        public CategoryRepository( ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
          return await _context.Categories.ToListAsync();

        }
     public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
     public async Task UpdateCategoryAsync(Category category)
        {
           _context.Categories.Update(category);
            await _context.SaveChangesAsync();  
        }
     public async Task DeleteCategoryAsync(int id)
        {
           var category= await _context.Categories.FindAsync(id);
            if (category != null)
            {
            _context.Categories.Remove(category);
             await _context.SaveChangesAsync();

            }


        }
        public async Task AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

        }
    }
}
