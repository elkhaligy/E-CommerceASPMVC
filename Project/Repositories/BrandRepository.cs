using Microsoft.EntityFrameworkCore;
using Project.Contract;

namespace Project.Repositories
{
    public class BrandRepository:IBrandRepository
    {
        private readonly ApplicationContext _context;

        public BrandRepository(ApplicationContext context)
        {
            _context = context;
        }
       public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.ToListAsync();

        }
       public async Task<Brand> GetByIdAsync(int id)
        {
          return await _context.Brands.FindAsync(id);
            

        }
       public async Task UpdateBrandAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();

        }
       public async Task DeleteBrandAsync(int id)
        {
            var brand =await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                await _context.SaveChangesAsync();

            }
        }
       public async Task AddBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

        }
    }
}
