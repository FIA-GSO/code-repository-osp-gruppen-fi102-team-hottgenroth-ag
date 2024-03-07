using System;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public class ArticleData : IArticleData
    {
        public string ArticleGuid { get; set; }
        public string ArticleName { get; set; }
        public string Description { get; set; }
        public int? Gtin { get; set; }
        public string Unit { get; set; }
        public double? Position { get; set; }
        public string Status { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Guid BoxGuid { get; set; }
        public Guid ArticleBoxAssignment { get; set; }
    }
}