using System;
using System.Collections.Generic;

namespace HuynmHE176493.Data.Models
{
    public partial class NewsArticle
    {
        public int NewsArticleId { get; set; }  // ✅ Đảm bảo có thuộc tính này
        public string NewsTitle { get; set; } = null!;
        public string Headline { get; set; } = null!;
        public string NewsSource { get; set; } = null!;
        public string NewsContent { get; set; } = null!;
        public bool NewsStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? UpdatedById { get; set; } // ✅ Đảm bảo có thuộc tính này
        public int CategoryId { get; set; }  // ✅ Đảm bảo có thuộc tính này

        // Khóa ngoại
        public virtual Category Category { get; set; } = null!;  // ✅ Đảm bảo có thuộc tính này
        public virtual SystemAccount CreatedBy { get; set; } = null!;
        public virtual SystemAccount? UpdatedBy { get; set; } // ✅ Đảm bảo có thuộc tính này

        // Quan hệ nhiều-nhiều với Tag
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();  // ✅ Đảm bảo có thuộc tính này
    }
}
