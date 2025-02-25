using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Data.IRepository
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        Tag GetById(int id);
        void Add(Tag tag);
        void Update(Tag tag);
        void Delete(int id);
        bool HasNewsArticles(int tagId); // Kiểm tra xem tag có bài viết liên kết không
    }
}