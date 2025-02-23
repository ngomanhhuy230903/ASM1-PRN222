using Assignment1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HuynmHE176493.Data.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FunewsManagementContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(FunewsManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
