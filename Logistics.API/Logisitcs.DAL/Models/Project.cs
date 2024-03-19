using System;
using System.Collections.Generic;

namespace Logisitcs.DAL.Models;

public partial class Project
{
    public string ProjectGuid { get; set; }

    public string ProjectName { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<Transportbox> Transportboxes { get; set; } = new List<Transportbox>();
}