using System;
using System.Collections.Generic;

namespace Ngô_Mạnh_Huy_HE176493_SE1817_NET_A01.Models;

public partial class SystemAccount
{
    public int AccountId { get; set; }

    public string AccountName { get; set; } = null!;

    public string AccountEmail { get; set; } = null!;

    public int AccountRole { get; set; }

    public string AccountPassword { get; set; } = null!;

    public virtual ICollection<NewsArticle> NewsArticleCreatedBies { get; set; } = new List<NewsArticle>();

    public virtual ICollection<NewsArticle> NewsArticleUpdatedBies { get; set; } = new List<NewsArticle>();
}
