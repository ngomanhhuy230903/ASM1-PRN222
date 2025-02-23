using System.ComponentModel.DataAnnotations;

namespace HuynmHE176493.Data.Models
{
    public class SystemAccount
    {
        public int AccountId { get; set; }  // ✅ Đảm bảo có thuộc tính này
        public string AccountEmail { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;

        public int AccountRole { get; set; } // 1 = Staff, 2 = Lecturer, 3 = Admin

        public bool IsActive { get; set; } = true; // ✅ Mặc định tài khoản được kích hoạt

        public virtual ICollection<NewsArticle> NewsArticleCreatedBies { get; set; } = new List<NewsArticle>();
        public virtual ICollection<NewsArticle> NewsArticleUpdatedBies { get; set; } = new List<NewsArticle>();

    }

    public enum AccountRole
    {
        Staff = 1,
        Lecturer = 2,
        Admin = 3
    }
}
