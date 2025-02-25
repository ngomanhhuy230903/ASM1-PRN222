using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HuynmHE176493.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FunewsManagementContext _context;

        public CategoryRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Include(c => c.NewsArticles).ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Include(c => c.NewsArticles).FirstOrDefault(c => c.CategoryId == id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            var existingCategory = _context.Categories.Find(category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.CategoryDescription = category.CategoryDescription;
                existingCategory.ParentCategoryId = category.ParentCategoryId;
                existingCategory.IsActive = category.IsActive;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public bool HasNewsArticles(int categoryId)
        {
            return _context.NewsArticles.Any(n => n.CategoryId == categoryId);
        }
    }
}