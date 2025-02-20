using System;
using System.Collections.Generic;

namespace Ngô_Mạnh_Huy_HE176493_SE1817_NET_A01.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public string? Note { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
