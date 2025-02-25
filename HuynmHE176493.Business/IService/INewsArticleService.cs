using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Business.IService
{
    public interface INewsArticleService
    {
        IEnumerable<NewsArticle> GetAll();
        NewsArticle GetById(int id);
        void Add(NewsArticle article);
        void Update(NewsArticle article);
        void Delete(int id);
        IEnumerable<NewsArticle> Search(string keyword);
        IEnumerable<Category> GetCategories(); // ✅ Thêm phương thức lấy danh mục
        IEnumerable<NewsArticle> GetArticlesByCreator(int createdById); // Thêm phương thức mới
        IDictionary<string, int> GetReportData(DateTime startDate, DateTime endDate); // Thêm phương thức báo cáo
    }
}
