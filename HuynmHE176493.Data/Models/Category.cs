using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace HuynmHE176493.Data.Models
{
    public partial class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(255)]
        public string CategoryName { get; set; } = null!;

        public string? CategoryDescription { get; set; }

        public int? ParentCategoryId { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [ForeignKey("ParentCategoryId")]
        public virtual Category? ParentCategory { get; set; }

        public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();
        public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
    }
}
