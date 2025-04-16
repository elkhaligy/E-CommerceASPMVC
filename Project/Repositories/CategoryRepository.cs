using Microsoft.EntityFrameworkCore;
using Project.Contract;
using Project.Models;

namespace Project.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationContext context) : base(context) { }

    }
} 