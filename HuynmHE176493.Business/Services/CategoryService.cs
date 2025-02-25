using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public void Add(Category category)
        {
            _categoryRepository.Add(category);
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }

        public bool Delete(int id)
        {
            if (_categoryRepository.HasNewsArticles(id))
            {
                return false; // Không xóa nếu danh mục có bài viết liên kết
            }
            _categoryRepository.Delete(id);
            return true;
        }
    }
}