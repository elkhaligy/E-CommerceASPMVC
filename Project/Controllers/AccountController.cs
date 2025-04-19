using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.Contract;
using Project.Models;
using Project.ViewModel;

namespace Project.Controllers
{
    public class AccountController : Controller
    {

        private readonly ICustomerRepository _customerRepo;
        private readonly IPasswordHasher<Customer> _passwordHasher;
        public AccountController(ICustomerRepository customerRepo, IPasswordHasher<Customer> passwordHasher)
        {
            _customerRepo = customerRepo;
            _passwordHasher = passwordHasher;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingCustomer = await _customerRepo.GetByEmailAsync(model.Email);
            if (existingCustomer != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };

            customer.PasswordHash = _passwordHasher.HashPassword(customer, model.Password);
            _customerRepo.Add(customer);
            await _customerRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customer = await _customerRepo.GetByEmailAsync(model.Email);
            if (customer == null)
            {
                ModelState.AddModelError("Email", "Invalid email or password");
                return View(model);
            }
            var verificationResult = _passwordHasher.VerifyHashedPassword(customer, customer.PasswordHash!, model.Password);

            if (verificationResult == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim(ClaimTypes.GivenName, customer.FirstName),
                    new Claim(ClaimTypes.Surname, customer.LastName),
                    new Claim(ClaimTypes.Role, "Customer")
                };
                var idenitty = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(idenitty);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,  // Makes the cookie persistent
                    ExpiresUtc = model.RememberMe ? DateTime.UtcNow.AddDays(14) : (DateTime?)null // Set expiration for persistent cookies
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Strange bug here
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // To logout you need to clear the cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
