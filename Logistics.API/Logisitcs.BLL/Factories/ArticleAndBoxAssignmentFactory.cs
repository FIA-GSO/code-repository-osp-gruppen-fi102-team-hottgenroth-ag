using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using System;

namespace Logisitcs.BLL.Factories
{
    public class ArticleAndBoxAssignmentFactory : IArticleAndBoxAssignmentFactory
    {
        public ArticleAndBoxAssignment CreateAdd(IArticleData articleData)
        {
            return new ArticleAndBoxAssignment
                (articleData.ArticleGuid,
             articleData.ArticleName,
             articleData.Description,
             articleData.Gtin,
             articleData.Unit,
             articleData.ArticleBoxAssignment.ToString(),
             articleData.BoxGuid.ToString(),
             articleData.Position,
             DbCommandsState.GetStatusByName(articleData.Status),
             articleData.Quantity,
             articleData.ExpiryDate.ToString());
        }

        public ArticleAndBoxAssignment CreateUpdate(IArticleData articleData)
        {
            var expiryDate = articleData.ExpiryDate != null ? articleData.ExpiryDate.ToString() : null;
            return new ArticleAndBoxAssignment
            (articleData.ArticleGuid.ToString(),
             articleData.ArticleName,
             articleData.Description,
             articleData.Gtin,
             articleData.Unit,
             articleData.ArticleBoxAssignment.ToString(),
             articleData.BoxGuid.ToString(),
             articleData.Position,
             DbCommandsState.GetStatusByName(articleData.Status),
             articleData.Quantity,
             expiryDate
             );
        }
    }
}