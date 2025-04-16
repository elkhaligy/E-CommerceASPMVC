using Microsoft.AspNetCore.Mvc;
using Project.Contract;
using Project.ViewModel;

namespace Project.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: Customer/Index
        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllAsync();
            var viewModels = customers.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
            });
            return View(viewModels);
        }

        

    }
}
