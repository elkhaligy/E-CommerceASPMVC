using Project.Models;

namespace Project.Contract
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);
    }
}
