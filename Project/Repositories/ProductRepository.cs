
using Project.Contract;

namespace Project.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            _context=context;
        }
        public async Task addProductAsync(Product product)
        {
           await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

        }
        public void save()
        {
            _context.SaveChanges();
        }
        public void edit (Product product   ) { } 
        public void delete(Product product) { }
        public List<Product> getAll() 
        {
            return new List<Product>();
        }
        public void getById(int id)
        {
  
        }


    }
}
