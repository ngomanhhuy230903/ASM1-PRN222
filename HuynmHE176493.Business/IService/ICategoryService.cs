using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Business.IService
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        bool Delete(int id); // Trả về bool để báo hiệu xóa thành công hay không
    }
}