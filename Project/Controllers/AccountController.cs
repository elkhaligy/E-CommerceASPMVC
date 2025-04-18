using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        // [HttpPost]
        // public IActionResult Login(LoginViewModel model)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(model);
        //     }

        // }
    }
}
