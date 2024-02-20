using Logisitcs.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace Logisitcs.DAL.Models;

public partial class ArticleBoxAssignment
{
    public string AssignmentGuid { get; set; }

    public string ArticleGuid { get; set; }

    public string BoxGuid { get; set; }

    public double? Position { get; set; }

    public long? Status { get; set; }

    public long? Quantity { get; set; }

    public byte[] ExpiryDate { get; set; }

    public virtual Article Article { get; set; }

    public virtual Transportbox Box { get; set; }

    public virtual Status StatusNavigation { get; set; }
}
