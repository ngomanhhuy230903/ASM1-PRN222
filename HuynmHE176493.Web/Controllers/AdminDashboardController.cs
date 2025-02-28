using Microsoft.AspNetCore.Mvc;
using HuynmHE176493.Business.IService;
using System;
using HuynmHE176493.Web.Filters;
namespace HuynmHE176493.Web.Controllers
{
    [AdminAuthFilter]
    public class AdminDashboardController : Controller
    {
        private readonly INewsArticleService _newsArticleService;

        public AdminDashboardController(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public IActionResult Index()
        {
            int? userRole = HttpContext.Session.GetInt32("UserRole");

            // Kiểm tra nếu user không đăng nhập hoặc role không phải 0 (Admin) hoặc 1 (Staff)
            if (!userRole.HasValue || (userRole != 0 && userRole != 1))
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index", "Login");
            }

            ViewBag.UserRole = userRole.Value;
            return View();
        }

        // GET: Hiển thị form báo cáo
        public IActionResult Report()
        {
            int? userRole = HttpContext.Session.GetInt32("UserRole");

            // Kiểm tra nếu user không đăng nhập hoặc không phải Admin (role = 0)
            if (!userRole.HasValue || userRole != 0)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index", "Login");
            }

            // Mặc định hiển thị báo cáo cho 30 ngày gần nhất
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-30);
            var reportData = _newsArticleService.GetReportData(startDate, endDate);
            ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
            return View(reportData);
        }

        // POST: Tạo báo cáo theo khoảng thời gian
        [HttpPost]
        public IActionResult Report(DateTime startDate, DateTime endDate)
        {
            int? userRole = HttpContext.Session.GetInt32("UserRole");

            // Kiểm tra nếu user không đăng nhập hoặc không phải Admin (role = 0)
            if (!userRole.HasValue || userRole != 0)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index", "Login");
            }

            if (startDate > endDate)
            {
                TempData["ErrorMessage"] = "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.";
                return RedirectToAction("Report");
            }

            var reportData = _newsArticleService.GetReportData(startDate, endDate);
            ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
            return View(reportData);
        }
    }
}