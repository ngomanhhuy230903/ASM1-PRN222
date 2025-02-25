using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Business.IService
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAll();
        Tag GetById(int id);
        void Add(Tag tag);
        void Update(Tag tag);
        bool Delete(int id); // Trả về bool để báo hiệu xóa thành công hay không
    }
}