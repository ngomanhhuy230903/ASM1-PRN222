using Microsoft.AspNetCore.Mvc;
using HuynmHE176493.Business.IService;
using System;

namespace HuynmHE176493.Web.Controllers
{
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
            ViewBag.UserRole = userRole.HasValue ? userRole.Value : -1;
            return View();
        }

        // GET: Hiển thị form báo cáo
        public IActionResult Report()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 0) // Chỉ Admin (role = 0) được xem
            {
                return RedirectToAction("Index", "Home");
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
            if (HttpContext.Session.GetInt32("UserRole") != 0) // Chỉ Admin (role = 0) được xem
            {
                return RedirectToAction("Index", "Home");
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