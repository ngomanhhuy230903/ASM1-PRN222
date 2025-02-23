using Microsoft.AspNetCore.Mvc;

namespace HuynmHE176493.Web.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Hiển thị trang Dashboard
        }
    }
}
