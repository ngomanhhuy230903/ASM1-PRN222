using Assignment1.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Assignment1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Assignment1.Controllers
{
    public class AccountController : Controller
    {
        private readonly FunewsManagementContext _context;
        private readonly AdminAccountService _adminAccountService;

        public AccountController(FunewsManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _adminAccountService = new AdminAccountService(configuration);
        }

        public IActionResult AccessDenied()
        {
            return View();
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

            // 3️⃣ Chuyển đổi Role từ int sang string
            string role = user.AccountRole switch
            {
                1 => "Admin",
                2 => "Staff",
                3 => "Lecturer",
                _ => "Lecturer"
            };

            // 4️⃣ Phân quyền và tạo Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.AccountName),
                new Claim(ClaimTypes.Email, user.AccountEmail),
                new Claim(ClaimTypes.Role, role) // Lưu Role dưới dạng string
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            Console.WriteLine($"Đăng nhập thành công! Role: {role}"); // Debug xem role đúng chưa

            // 5️⃣ Chuyển hướng theo Role
            return role switch
            {
                "Admin" => RedirectToAction("ManageAccounts", "Admin"),
                "Staff" => RedirectToAction("ManageArticles", "Staff"),
                "Lecturer" => RedirectToAction("Index", "News"),
                _ => RedirectToAction("Login") // Nếu lỗi, quay lại trang đăng nhập
            };
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
