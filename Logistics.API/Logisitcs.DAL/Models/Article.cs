using System;
using System.Collections.Generic;

namespace Logisitcs.DAL.Models;

public partial class Article
{
    public string ArticleGuid { get; set; }

    public string ArticleName { get; set; }

    public string Description { get; set; }

    public long? Gtin { get; set; }

    public string Unit { get; set; }

    public virtual ICollection<ArticleBoxAssignment> ArticleBoxAssignments { get; set; } = new List<ArticleBoxAssignment>();
}
