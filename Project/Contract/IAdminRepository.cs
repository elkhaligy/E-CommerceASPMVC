using Project.Models;

namespace Project.Contract
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin?> GetByEmailAsync(string email);
    }
}
