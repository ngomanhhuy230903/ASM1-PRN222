using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HuynmHE176493.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _authService.Authenticate(email, password);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid email or password";
                return View();
            }

            HttpContext.Session.SetString("UserRole", user.AccountRole.ToString());
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
