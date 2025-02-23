using System.ComponentModel.DataAnnotations;

namespace HuynmHE176493.Data.Models
{
    public partial class SystemAccount
    {
        [Key]
        public int AccountId { get; set; }

        [Required, MaxLength(255)]
        public string AccountName { get; set; } = null!;

        [Required, MaxLength(255)]
        public string AccountEmail { get; set; } = null!;

        [Required]
        public int AccountRole { get; set; } // 1 = Staff, 2 = Lecturer, Admin từ appsettings.json

        [Required]
        public bool IsActive { get; set; } = true;

        [Required, MaxLength(255)]
        public string AccountPassword { get; set; } = null!;

        public virtual ICollection<NewsArticle> NewsArticleCreatedBies { get; set; } = new List<NewsArticle>();
        public virtual ICollection<NewsArticle> NewsArticleUpdatedBies { get; set; } = new List<NewsArticle>();
    }
}
