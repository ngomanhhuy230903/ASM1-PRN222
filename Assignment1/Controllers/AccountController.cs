using Assignment1.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Assignment1.Models;
using Microsoft.EntityFrameworkCore;
using Assignment1.Services;

namespace Assignment1.Controllers
{
    public class AccountController : Controller
    {
        private readonly FunewsManagementContext _context;
        private readonly IAdminAccountService _adminAccountService;

        public AccountController(FunewsManagementContext context, IAdminAccountService adminAccountService)
        {
            _context = context;
            _adminAccountService = adminAccountService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            SystemAccount user;

            // 1️⃣ Kiểm tra tài khoản Admin từ appsettings.json
            var admin = _adminAccountService.GetAdminAccount();
            if (model.Email == admin.AccountEmail && model.Password == admin.AccountPassword)
            {
                user = admin;
            }
            else
            {
                // 2️⃣ Kiểm tra trong Database
                user = await _context.SystemAccounts
                    .FirstOrDefaultAsync(a => a.AccountEmail == model.Email && a.AccountPassword == model.Password);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            // 3️⃣ Phân quyền theo Role
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.AccountName),
                new Claim(ClaimTypes.Email, user.AccountEmail),
                new Claim(ClaimTypes.Role, user.AccountRole.ToString()) // Lưu role dưới dạng string
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // 4️⃣ Chuyển hướng theo Role
            if (user.AccountRole == 1)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (user.AccountRole == 2)
            {
                return RedirectToAction("Index", "News");
            }
            else
            {
                return RedirectToAction("Index", "News"); // Giảng viên chỉ xem tin tức
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
