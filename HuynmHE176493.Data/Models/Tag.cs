using HuynmHE176493.Data.Models;
using System.ComponentModel.DataAnnotations;
namespace HuynmHE176493.Data.Models
{
    public partial class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required, MaxLength(255)]
        public string TagName { get; set; } = null!;

        public string? Note { get; set; }

        public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
    }
}    
