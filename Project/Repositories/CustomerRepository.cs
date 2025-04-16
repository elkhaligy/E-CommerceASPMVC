using Microsoft.EntityFrameworkCore;
using Project.Contract;
using Project.Models;

namespace Project.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationContext context) : base(context) {}

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
        }
        
    }
}
