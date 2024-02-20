using System;
using System.Collections.Generic;

namespace Logisitcs.DAL.Models;

public partial class Transportbox
{
    public string BoxGuid { get; set; }

    public long? Number { get; set; }

    public string Description { get; set; }

    public string ProjectGuid { get; set; }

    public string LocationTransport { get; set; }

    public string LocationHome { get; set; }

    public string LocationDeployment { get; set; }

    public virtual ICollection<ArticleBoxAssignment> ArticleBoxAssignments { get; set; } = new List<ArticleBoxAssignment>();

    public virtual Project Project { get; set; }
}
