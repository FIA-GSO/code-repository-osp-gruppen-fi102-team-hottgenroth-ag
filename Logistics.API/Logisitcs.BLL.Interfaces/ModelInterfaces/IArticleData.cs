using System;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public interface IArticleData
    {
        Guid ArticleGuid { get; set; }
        string ArticleName { get; set; }
        string Description { get; set; }
        int? Gtin { get; set; }
        string Unit { get; set; }
        double? Position { get; set; }
        string Status { get; set; }
        int? Quantity { get; set; }
        Guid BoxGuid { get; set; }
        Guid ArticleBoxAssignment { get; set; }
        DateTime? ExpiryDate { get; set; }
    }
}