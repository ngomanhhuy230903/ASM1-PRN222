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

        // ✅ Thêm phương thức tìm kiếm
        IEnumerable<NewsArticle> Search(string keyword);
    }
}
