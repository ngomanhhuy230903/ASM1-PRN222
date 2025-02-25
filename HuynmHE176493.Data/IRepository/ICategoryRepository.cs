using System.Linq.Expressions;
using HuynmHE176493.Data.Models;

namespace HuynmHE176493.Data.IRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        
        Category GetById(int id);
        void Add(Category entity);
        void Update(Category entity);
        void Delete(int id);
        bool HasNewsArticles(int categoryId);
    }
}
