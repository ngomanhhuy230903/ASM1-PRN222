using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HuynmHE176493.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Hiển thị danh sách danh mục
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được xem
            {
                return RedirectToAction("Index", "NewsArticle");
            }
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        // GET: Tạo danh mục mới
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được tạo
            {
                return RedirectToAction("Index");
            }
            ViewBag.ParentCategories = new SelectList(_categoryService.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Tạo danh mục mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _categoryService.Add(category);
                return RedirectToAction("Index");
            }
            ViewBag.ParentCategories = new SelectList(_categoryService.GetAll(), "CategoryId", "CategoryName");
            return View(category);
        }

        // GET: Chỉnh sửa danh mục
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();
            ViewBag.ParentCategories = new SelectList(_categoryService.GetAll(), "CategoryId", "CategoryName");
            return View(category);
        }

        // POST: Chỉnh sửa danh mục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _categoryService.Update(category);
                return RedirectToAction("Index");
            }
            ViewBag.ParentCategories = new SelectList(_categoryService.GetAll(), "CategoryId", "CategoryName");
            return View(category);
        }

        // GET: Xác nhận xóa danh mục
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: Xóa danh mục
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            var success = _categoryService.Delete(id);
            if (!success)
            {
                TempData["ErrorMessage"] = "Không thể xóa danh mục vì vẫn còn bài viết liên kết.";
            }
            return RedirectToAction("Index");
        }
    }
}