using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HuynmHE176493.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly FunewsManagementContext _context;

        public TagRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.Include(t => t.NewsArticles).ToList();
        }

        public Tag GetById(int id)
        {
            return _context.Tags.Include(t => t.NewsArticles).FirstOrDefault(t => t.TagId == id);
        }

        public void Add(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void Update(Tag tag)
        {
            var existingTag = _context.Tags.Find(tag.TagId);
            if (existingTag != null)
            {
                existingTag.TagName = tag.TagName;
                existingTag.Note = tag.Note; // Sử dụng Note thay vì Description
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }
        }

        public bool HasNewsArticles(int tagId)
        {
            return _context.NewsArticles.Any(n => n.Tags.Any(t => t.TagId == tagId));
        }
    }
}