using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Business.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepository;

        public NewsArticleService(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return _newsArticleRepository.GetAll();
        }

        public NewsArticle GetById(int id)
        {
            return _newsArticleRepository.GetById(id);
        }

        public void Add(NewsArticle article)
        {
            _newsArticleRepository.Add(article);
        }

        public void Update(NewsArticle article)
        {
            _newsArticleRepository.Update(article);
        }

        public void Delete(int id)
        {
            _newsArticleRepository.Delete(id);
        }

        // ✅ Thêm chức năng tìm kiếm
        public IEnumerable<NewsArticle> Search(string keyword)
        {
            return _newsArticleRepository.Search(keyword);
        }
        public IEnumerable<Category> GetCategories()
        {
            return _newsArticleRepository.GetCategories();
        }
        public IEnumerable<NewsArticle> GetArticlesByCreator(int createdById)
        {
            return _newsArticleRepository.GetArticlesByCreator(createdById);
        }
        public IDictionary<string, int> GetReportData(DateTime startDate, DateTime endDate)
        {
            return _newsArticleRepository.GetReportData(startDate, endDate);
        }
    }
}
