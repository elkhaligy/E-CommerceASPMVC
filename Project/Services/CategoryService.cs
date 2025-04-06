using Project.Contract;

namespace Project.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository;
        public CategoryService(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public async Task <IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _CategoryRepository.GetAllCategoriesAsync();

        }
    }
}
