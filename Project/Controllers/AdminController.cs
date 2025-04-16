using Microsoft.AspNetCore.Mvc;
using Project.Contract;
using Project.ViewModel;
namespace Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

         // GET: Admin/Index
        public async Task<IActionResult> Index()
        {
            var admins = await _adminRepository.GetAllAsync();
            var viewModels = admins.Select(a => new AdminViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
            });

            return View(viewModels);
        }


    }
}
