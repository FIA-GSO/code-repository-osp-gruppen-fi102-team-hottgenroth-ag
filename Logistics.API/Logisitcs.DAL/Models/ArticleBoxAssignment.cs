
namespace Logisitcs.DAL.Models;

public partial class ArticleBoxAssignment
{
    public string AssignmentGuid { get; set; }

    public string ArticleGuid { get; set; }

    public string BoxGuid { get; set; }

    public double? Position { get; set; }

    public int? Status { get; set; }

    public int? Quantity { get; set; }

    public string ExpiryDate { get; set; }

    public virtual Article Article { get; set; }

    public virtual Transportbox Box { get; set; }

    public virtual Status StatusNavigation { get; set; }
}