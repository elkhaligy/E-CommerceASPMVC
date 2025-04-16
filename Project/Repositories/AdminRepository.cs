using Microsoft.EntityFrameworkCore;
using Project.Contract;
using Project.Models;

namespace Project.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext context) : base(context) {}

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            Admin? admin = await _dbSet.FirstOrDefaultAsync(a => a.Email == email);
            return admin;
        }
    }
}
