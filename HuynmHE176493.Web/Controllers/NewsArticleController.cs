using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HuynmHE176493.Web.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;

        public NewsArticleController(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        // ✅ Thêm chức năng tìm kiếm vào danh sách tin tức
        public IActionResult Index(string searchKeyword)
        {
            var articles = string.IsNullOrEmpty(searchKeyword)
                ? _newsArticleService.GetAll()
                : _newsArticleService.Search(searchKeyword);

            return View(articles);
        }

        // ✅ Chi tiết bài viết
        public IActionResult Details(int id)
        {
            var article = _newsArticleService.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserRole") != (int)AccountRole.Staff)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(NewsArticle article)
        {
            if (HttpContext.Session.GetInt32("UserRole") != (int)AccountRole.Staff)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _newsArticleService.Add(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // ✅ Sửa bài viết (GET)
        public IActionResult Edit(int id)
        {
            var article = _newsArticleService.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // ✅ Sửa bài viết (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewsArticle article)
        {
            if (ModelState.IsValid)
            {
                _newsArticleService.Update(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // ✅ Xóa bài viết (GET)
        public IActionResult Delete(int id)
        {
            var article = _newsArticleService.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // ✅ Xóa bài viết (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _newsArticleService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
