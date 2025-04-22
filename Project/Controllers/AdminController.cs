using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project.Contract;
using Project.Models;
using Project.ViewModel;
using System.Security.Claims;
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
        public  IActionResult signIn()
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
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.GivenName, admin.FirstName),
                    new Claim(ClaimTypes.Surname, admin.LastName),
                    new Claim(ClaimTypes.Role, "Admin")
            };
            var idenitty = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(idenitty);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,  // Makes the cookie persistent
                ExpiresUtc = model.RememberMe ? DateTime.UtcNow.AddDays(14) : (DateTime?)null // Set expiration for persistent cookies
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            // Set session

            HttpContext.Session.SetString("AdminFirstName", admin.FirstName);
            Console.WriteLine(HttpContext.Session.Id);

            return RedirectToAction("Index");

        }




    }
}
