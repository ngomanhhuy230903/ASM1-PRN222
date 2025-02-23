using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using System.Collections.Generic;
using System.Linq;

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
            _context.NewsArticles.Add(article);
            _context.SaveChanges();
        }

        public void Update(NewsArticle article)
        {
            _context.NewsArticles.Update(article);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var article = _context.NewsArticles.Find(id);
            if (article != null)
            {
                _context.NewsArticles.Remove(article);
                _context.SaveChanges();
            }
        }

        // ✅ Thêm chức năng tìm kiếm
        public IEnumerable<NewsArticle> Search(string keyword)
        {
            return _context.NewsArticles
                .Where(n => n.NewsTitle.Contains(keyword) || n.NewsSource.Contains(keyword))
                .ToList();
        }
    }
}
