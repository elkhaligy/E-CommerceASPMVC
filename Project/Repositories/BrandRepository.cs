using Microsoft.EntityFrameworkCore;
using Project.Contract;

namespace Project.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationContext context) : base(context) { }
      
    }
}
