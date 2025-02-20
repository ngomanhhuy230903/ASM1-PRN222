using Microsoft.AspNetCore.Mvc;
using System;
using Assignment1.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Controllers
{
    public class NewsController : Controller
    {
        private readonly FunewsManagementContext _context;

        public NewsController(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var activeNews = await _context.NewsArticles
                .Where(n => n.NewsStatus == true)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();

            return View(activeNews);
        }
    }
}
