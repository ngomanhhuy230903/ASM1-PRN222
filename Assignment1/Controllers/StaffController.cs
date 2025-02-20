using Assignment1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {
        private readonly FunewsManagementContext _context;

        public StaffController(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ManageArticles()
        {
            var articles = await _context.NewsArticles.ToListAsync();
            return View(articles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(NewsArticle article)
        {
            _context.NewsArticles.Add(article);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageArticles");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            var article = await _context.NewsArticles.FindAsync(articleId);
            if (article == null) return NotFound();

            _context.NewsArticles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageArticles");
        }
    }
}
