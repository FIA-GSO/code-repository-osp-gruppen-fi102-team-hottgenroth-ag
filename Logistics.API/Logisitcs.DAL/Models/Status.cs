using System.Collections.Generic;

namespace Logisitcs.DAL.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<ArticleBoxAssignment> ArticleBoxAssignments { get; set; } = new List<ArticleBoxAssignment>();
}