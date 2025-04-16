using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            System.Console.WriteLine(returnUrl);
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
