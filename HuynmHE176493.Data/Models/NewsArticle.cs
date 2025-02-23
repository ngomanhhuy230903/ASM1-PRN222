using HuynmHE176493.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace HuynmHE176493.Data.Models
{
    public partial class NewsArticle
    {
        [Key]
        public int NewsArticleId { get; set; }

        [Required, MaxLength(500)]
        public string NewsTitle { get; set; } = null!;

        [MaxLength(1000)]
        public string? Headline { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public string NewsContent { get; set; } = null!;

        [MaxLength(255)]
        public string? NewsSource { get; set; }

        [Required]
        public bool NewsStatus { get; set; } = true;

        public int CategoryId { get; set; }
        public int CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;

        [ForeignKey("CreatedById")]
        public virtual SystemAccount CreatedBy { get; set; } = null!;

        [ForeignKey("UpdatedById")]
        public virtual SystemAccount? UpdatedBy { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
