using Microsoft.EntityFrameworkCore;
using HuynmHE176493.Data.Models;
using System.Linq.Expressions;
using HuynmHE176493.Data.IRepository;

namespace HuynmHE176493.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FunewsManagementContext _context;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(FunewsManagementContext context)
        {
            _context = context;
            _dbSet = context.Set<Category>();
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbSet.ToList();
        }

        public Category GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(Category entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Category entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
    }
}
