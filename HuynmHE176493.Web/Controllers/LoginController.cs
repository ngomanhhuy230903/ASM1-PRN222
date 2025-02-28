using Microsoft.AspNetCore.Mvc;
using HuynmHE176493.Business.IService;
using Microsoft.Extensions.Configuration;

namespace HuynmHE176493.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public LoginController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // Lấy thông báo lỗi từ Cookie nếu có
            if (Request.Cookies["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = Request.Cookies["ErrorMessage"];
                Response.Cookies.Delete("ErrorMessage"); // Xóa Cookie sau khi dùng
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _accountService.ValidateUser(email, password);

            // Nếu là Admin mặc định trong appsettings.json
            if (user == null && _accountService.IsDefaultAdmin(email, password))
            {
                HttpContext.Session.SetInt32("UserRole", 0); // 0 là quyền Admin
                HttpContext.Session.SetString("UserEmail", email);
                return RedirectToAction("Index", "AdminDashboard");
            }

            // Nếu tài khoản tồn tại trong DB
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserID", user.AccountId);
                HttpContext.Session.SetInt32("UserRole", user.AccountRole);
                HttpContext.Session.SetString("UserEmail", user.AccountEmail);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Email hoặc mật khẩu không đúng!";
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}