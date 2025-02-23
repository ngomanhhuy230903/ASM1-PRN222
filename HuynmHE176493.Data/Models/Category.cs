using System.Collections.Generic;

namespace HuynmHE176493.Data.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }  // ✅ Kiểm tra có dòng này không
        public string CategoryName { get; set; } = null!;
        public bool IsActive { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();
        public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
    }
}
