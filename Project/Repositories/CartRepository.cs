using Microsoft.EntityFrameworkCore;
using Project.Contract;
using Project.Models;

namespace Project.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationContext context) : base(context)
        {
        }

    }
}