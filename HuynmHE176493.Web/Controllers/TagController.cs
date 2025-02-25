using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HuynmHE176493.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1)
            {
                return RedirectToAction("Index", "NewsArticle");
            }
            var tags = _tagService.GetAll();
            return View(tags);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _tagService.Add(tag);
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            var tag = _tagService.GetById(id);
            if (tag == null) return NotFound();
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _tagService.Update(tag);
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            var tag = _tagService.GetById(id);
            if (tag == null) return NotFound();
            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1)
            {
                return RedirectToAction("Index");
            }
            var success = _tagService.Delete(id);
            if (!success)
            {
                TempData["ErrorMessage"] = "Không thể xóa tag vì vẫn còn bài viết liên kết.";
            }
            return RedirectToAction("Index");
        }
    }
}