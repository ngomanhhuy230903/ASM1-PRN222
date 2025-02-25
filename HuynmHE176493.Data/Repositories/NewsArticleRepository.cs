using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HuynmHE176493.Data.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly FunewsManagementContext _context;

        public NewsArticleRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return _context.NewsArticles.ToList();
        }

        public NewsArticle GetById(int id)
        {
            return _context.NewsArticles.FirstOrDefault(n => n.NewsArticleId == id);
        }

        public void Add(NewsArticle article)
        {
            if (article == null) return;
            _context.NewsArticles.Add(article); // Sử dụng trực tiếp đối tượng article
            _context.SaveChanges();
        }

        public void Update(NewsArticle article)
        {
            var existingArticle = _context.NewsArticles.Find(article.NewsArticleId);
            if (existingArticle != null)
            {
                existingArticle.NewsTitle = article.NewsTitle;
                existingArticle.Headline = article.Headline;
                existingArticle.NewsContent = article.NewsContent;
                existingArticle.NewsSource = article.NewsSource;
                existingArticle.CategoryId = article.CategoryId;
                existingArticle.NewsStatus = article.NewsStatus;
                existingArticle.UpdatedById = article.UpdatedById;
                existingArticle.ModifiedDate = article.ModifiedDate;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var article = _context.NewsArticles.Include(a => a.Tags)
                                              .FirstOrDefault(a => a.NewsArticleId == id);
            if (article != null)
            {
                _context.Tags.RemoveRange(article.Tags); // Xóa các Tags liên quan
                _context.NewsArticles.Remove(article);
                _context.SaveChanges();
            }
        }

        public IEnumerable<NewsArticle> Search(string keyword)
        {
            return _context.NewsArticles
                .Where(n => n.NewsTitle.Contains(keyword) || n.NewsSource.Contains(keyword))
                .ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.Where(c => c.IsActive).ToList();
        }
        public IEnumerable<NewsArticle> GetArticlesByCreator(int createdById)
        {
            return _context.NewsArticles
                .Include(n => n.Category) // Include Category để hiển thị tên danh mục
                .Where(n => n.CreatedById == createdById && n.ModifiedDate == null) // Chỉ lấy bài viết chưa sửa
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }
        public IDictionary<string, int> GetReportData(DateTime startDate, DateTime endDate)
        {
            var articles = _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .ToList();

            var report = new Dictionary<string, int>
    {
        { "TotalArticles", articles.Count },
        { "ActiveArticles", articles.Count(a => a.NewsStatus) },
        { "InactiveArticles", articles.Count(a => !a.NewsStatus) },
        { "ArticlesCreated", articles.Count(a => a.ModifiedDate == null) },
        { "ArticlesUpdated", articles.Count(a => a.ModifiedDate != null) },
    };

            // Thống kê theo danh mục
            var categoryCounts = articles
                .GroupBy(a => a.Category.CategoryName)
                .ToDictionary(g => $"Category_{g.Key}", g => g.Count());
            foreach (var kvp in categoryCounts)
            {
                report.Add(kvp.Key, kvp.Value);
            }

            // Thống kê theo người tạo
            var creatorCounts = articles
                .GroupBy(a => a.CreatedBy.AccountName)
                .ToDictionary(g => $"Creator_{g.Key}", g => g.Count());
            foreach (var kvp in creatorCounts)
            {
                report.Add(kvp.Key, kvp.Value);
            }

            return report;
        }
    }
}