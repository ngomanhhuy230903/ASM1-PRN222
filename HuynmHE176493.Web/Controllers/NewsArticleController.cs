using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.Models;
using HuynmHE176493.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HuynmHE176493.Web.Controllers
{
    [AdminAuthFilter]
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public NewsArticleController(INewsArticleService newsArticleService, IEmailService emailService, IConfiguration configuration)
        {
            _newsArticleService = newsArticleService;
            _emailService = emailService;
            _configuration = configuration;
        }

        // GET: Hiển thị danh sách bài viết (có tìm kiếm)
        public IActionResult Index(string searchKeyword)
        {
            var articles = string.IsNullOrEmpty(searchKeyword)
                ? _newsArticleService.GetAll()
                : _newsArticleService.Search(searchKeyword);
            ViewBag.Categories = _newsArticleService.GetCategories().ToDictionary(c => c.CategoryId, c => c.CategoryName);
            return View(articles);
        }

        // GET: Chi tiết bài viết
        public IActionResult Details(int id)
        {
            var article = _newsArticleService.GetById(id);
            if (article == null) return NotFound();
            ViewBag.Categories = _newsArticleService.GetCategories().ToDictionary(c => c.CategoryId, c => c.CategoryName);
            return View(article);
        }

        // GET: Tạo bài viết mới
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được tạo
            {
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_newsArticleService.GetCategories(), "CategoryId", "CategoryName"); // Chỉ lấy categories có IsActive = true
            return View();
        }

        // POST: Tạo bài viết mới
        [HttpPost]
        public async Task<IActionResult> Create(NewsArticle article)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được tạo
            {
                return RedirectToAction("Index");
            }

            int? userId = HttpContext.Session.GetInt32("UserID");
            if (!userId.HasValue) // Kiểm tra nếu chưa đăng nhập
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Categories = new SelectList(_newsArticleService.GetCategories(), "CategoryId", "CategoryName");
            article.CreatedById = userId.Value;
            article.CreatedDate = DateTime.Now;
            _newsArticleService.Add(article);

            // Gửi email thông báo cho Admin
            var adminEmail = _configuration["EmailSettings:AdminEmail"];
            var author = _newsArticleService.GetById(article.NewsArticleId)?.CreatedBy?.AccountName ?? "Unknown";
            var articleUrl = Url.Action("Details", "NewsArticle", new { id = article.NewsArticleId }, Request.Scheme);
            var emailSubject = "New Article Published";
            var emailBody = $@"
                <h3>New Article Notification</h3>
                <p>A new article has been published:</p>
                <ul>
                    <li><strong>Title:</strong> {article.NewsTitle}</li>
                    <li><strong>Author:</strong> {author}</li>
                    <li><strong>Link:</strong> <a href='{articleUrl}'>View Article</a></li>
                </ul>
                <p>Regards,<br>FUNews Management System</p>";

            try
            {
                await _emailService.SendEmailAsync(adminEmail, emailSubject, emailBody);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần (không làm gián đoạn quá trình tạo bài viết)
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }

            return RedirectToAction("Index");
        }

        // GET: Chỉnh sửa bài viết
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được sửa
            {
                return RedirectToAction("Index");
            }

            var article = _newsArticleService.GetById(id);
            if (article == null) return NotFound();

            ViewBag.Categories = new SelectList(_newsArticleService.GetCategories(), "CategoryId", "CategoryName"); // Chỉ lấy categories có IsActive = true
            return View(article);
        }

        // POST: Chỉnh sửa bài viết
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewsArticle article)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được sửa
            {
                return RedirectToAction("Index");
            }

            int? userId = HttpContext.Session.GetInt32("UserID");
            if (!userId.HasValue) // Kiểm tra nếu chưa đăng nhập
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Categories = new SelectList(_newsArticleService.GetCategories(), "CategoryId", "CategoryName"); // Chỉ lấy categories có IsActive = true
            article.UpdatedById = userId.Value; // Gán ID người sửa
            article.ModifiedDate = DateTime.Now; // Cập nhật thời gian sửa
            _newsArticleService.Update(article);
            return RedirectToAction("Index");
        }

        // GET: Xác nhận xóa bài viết
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được xóa
            {
                return RedirectToAction("Index");
            }

            var article = _newsArticleService.GetById(id);
            if (article == null) return NotFound();
            return View(article);
        }

        // POST: Xóa bài viết
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được xóa
            {
                return RedirectToAction("Index");
            }

            var article = _newsArticleService.GetById(id);
            if (article != null)
            {
                _newsArticleService.Delete(id);
            }
            return RedirectToAction("Index");
        }
        public IActionResult History()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được xem
            {
                return RedirectToAction("Index", "Home");
            }

            int? userId = HttpContext.Session.GetInt32("UserID");
            if (!userId.HasValue) // Kiểm tra nếu chưa đăng nhập
            {
                return RedirectToAction("Index", "Login");
            }

            var articles = _newsArticleService.GetArticlesByCreator(userId.Value);
            return View(articles);
        }
    }
}