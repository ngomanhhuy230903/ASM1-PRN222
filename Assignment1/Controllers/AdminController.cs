using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly FunewsManagementContext _context;

        public AdminController(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ManageAccounts()
        {
            var users = await _context.SystemAccounts.ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(int accountId, int newRole)
        {
            var user = await _context.SystemAccounts.FindAsync(accountId);
            if (user == null) return NotFound();

            user.AccountRole = newRole;
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageAccounts");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            var user = await _context.SystemAccounts.FindAsync(accountId);
            if (user == null) return NotFound();

            _context.SystemAccounts.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageAccounts");
        }
        [HttpGet]
        public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var report = await _context.NewsArticles
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();

            return View(report);
        }

    }
}
