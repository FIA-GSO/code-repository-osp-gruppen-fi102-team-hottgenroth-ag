using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using Logisitcs.DAL.Models;
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
                status = DbCommandsState.GetStatusById(articleAndBoxAssignment.Status);
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

        public IArticleData Create(Article article)
        {
           return new ArticleData
           {
              ArticleGuid = article.ArticleGuid,
              ArticleName = article.ArticleName,
              Description = article.Description,
              Gtin = (int?)article.Gtin,
              ExpiryDate = null,
              Quantity = null,
              Position = null,
              Status = DbCommandsState.GetStatusById(7),
              Unit = article.Unit,
              BoxGuid = Guid.Empty,
              ArticleBoxAssignment = Guid.Empty
           };
        }
    }
}