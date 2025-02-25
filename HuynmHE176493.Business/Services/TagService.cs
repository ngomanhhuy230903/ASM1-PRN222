using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Business.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }

        public Tag GetById(int id)
        {
            return _tagRepository.GetById(id);
        }

        public void Add(Tag tag)
        {
            _tagRepository.Add(tag);
        }

        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);
        }

        public bool Delete(int id)
        {
            if (_tagRepository.HasNewsArticles(id))
            {
                return false; // Không xóa nếu tag có bài viết liên kết
            }
            _tagRepository.Delete(id);
            return true;
        }
    }
}