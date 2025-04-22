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
        // GET: Admin/SignIn 
        public async Task<IActionResult> signIn()
        {
            return View("Sign-in");
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Sign-in", model);

            var admin = await _adminRepository.GetByEmailAsync(model.Email);

            if (admin == null || admin.Password != model.Password)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View("Sign-in", model);
            }

            // Set session
            HttpContext.Session.SetInt32("AdminId", admin.Id);
            HttpContext.Session.SetString("AdminEmail", admin.Email);
            Console.WriteLine(HttpContext.Session.Id);
            return RedirectToAction("Index");
        }




    }
}
