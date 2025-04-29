using Microsoft.EntityFrameworkCore;
using Project.Contract;
using Project.DTO;
using Project.Models;

namespace Project.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        //public async Task<Product?> GetByIdAsync(int id)
        //{
        //    // We can get rid of these includes later by using lazy loading
        //    return await _context.Products
        //        .Include(p => p.Category)
        //        .Include(p => p.Brand)
        //        .Include(p => p.Admin)
        //        .Include(p => p.Images)
        //        .Include(p => p.Tags)
        //        .FirstOrDefaultAsync(p => p.ProductId == id);
        //}

        //public async Task<IEnumerable<Product>> GetAllAsync()
        //{
        //    return await _context.Products
        //        .Include(p => p.Category)
        //        .Include(p => p.Brand)
        //        .Include(p => p.Admin)
        //        .Include(p => p.Images)
        //        .Include(p => p.Tags)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Product>?> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Admin)
                .Include(p => p.Images)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>?> GetByBrandIdAsync(int brandId)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Admin)
                .Include(p => p.Images)
                .Where(p => p.BrandId == brandId)
                .ToListAsync();

        }

        public async Task<IEnumerable<Product>?> GetByAdminIdAsync(int adminId)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Admin)
                .Include(p => p.Images)
                .Where(p => p.AdminId == adminId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>?> SearchByNameAsync(string name)
        {   
            if (String.IsNullOrWhiteSpace(name))
            {
                return await _dbSet.ToListAsync();
            }
            return await _dbSet
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>?> FilterByCategoryAndBrandAsync(int? categoryId, int? brandId)
        {
            if (categoryId == null && brandId == null)
            {
                return await _dbSet.ToListAsync();
            }
            else if (categoryId != null && brandId == null)
            {
                return await _dbSet.Where(product => product.CategoryId == categoryId)
                    .ToListAsync();
            }
            else if (categoryId == null && brandId != null)
            {
                return await _dbSet.Where(product => product.BrandId == brandId)
                    .ToListAsync();
            }
            else
            {
                return await _dbSet.Where(product => product.CategoryId == categoryId && product.BrandId == brandId)
                    .ToListAsync();
            }
        }
        //paging
    
            public async Task<PagedResult<Product>> GetPagedProductsAsync(int pageNumber, int pageSize)
            {
            var totalItems = await _dbSet.CountAsync();

            var items = await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Admin)
                .Include(p => p.Images)
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        


             }


    //public async Task AddAsync(Product product)
    //{
    //    await _context.Products.AddAsync(product);
    //    await SaveChangesAsync();
    //}

    //public async Task UpdateAsync(Product product)
    //{
    //    _context.Products.Update(product);
    //    await SaveChangesAsync();
    //}

    //public async Task DeleteAsync(Product product)
    //{
    //    _context.Products.Remove(product);
    //    await SaveChangesAsync();
    //}

    //public async Task SaveChangesAsync()
    //{
    //    await _context.SaveChangesAsync();
    //}
}
} 