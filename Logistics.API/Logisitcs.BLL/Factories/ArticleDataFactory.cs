using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using System;

namespace Logisitcs.BLL.Factories
{
    public class ArticleDataFactory : IArticleDataFactory
    {
        public IArticleData Create(ArticleAndBoxAssignment articleAndBoxAssignment)
        {
            string status = string.Empty;

            if (articleAndBoxAssignment.Status != null)
            {
                status = DBCommands.GetStatusById(articleAndBoxAssignment.Status);
            }
            return new ArticleData
            {
                ArticleGuid = articleAndBoxAssignment.ArticleGuid,
                ArticleName = articleAndBoxAssignment.ArticleName,
                Description = articleAndBoxAssignment.Description,
                Gtin = (int?)articleAndBoxAssignment.Gtin,
                ExpiryDate = articleAndBoxAssignment.ExpireDate != null ? DateTime.Parse(articleAndBoxAssignment.ExpireDate) : null,
                Quantity = articleAndBoxAssignment.Quantity,
                Position = articleAndBoxAssignment.Position,
                Status = status,
                Unit = articleAndBoxAssignment.Unit,
                BoxGuid = Guid.Parse(articleAndBoxAssignment.BoxGuid),
                ArticleBoxAssignment = Guid.Parse(articleAndBoxAssignment.AssignmentGuid)
            };
        }
    }
}