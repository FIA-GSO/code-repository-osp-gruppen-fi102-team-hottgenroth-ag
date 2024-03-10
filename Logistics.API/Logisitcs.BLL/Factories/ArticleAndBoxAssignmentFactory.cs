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
                (Guid.NewGuid().ToString(),
             articleData.ArticleName,
             articleData.Description,
             articleData.Gtin,
             articleData.Unit,
             Guid.NewGuid().ToString(),
             articleData.BoxGuid.ToString(),
             articleData.Position,
             DBCommands.GetStatusByName(articleData.Status),
             articleData.Quantity,
             articleData.ExpiryDate.ToString());
        }

        public ArticleAndBoxAssignment CreateUpdate(IArticleData articleData)
        {
            return new ArticleAndBoxAssignment
            (articleData.ArticleGuid.ToString(),
             articleData.ArticleName,
             articleData.Description,
             articleData.Gtin,
             articleData.Unit,
             articleData.ArticleBoxAssignment.ToString(),
             articleData.BoxGuid.ToString(),
             articleData.Position,
             DBCommands.GetStatusByName(articleData.Status),
             articleData.Quantity,
             articleData.ExpiryDate.ToString());
        }
    }
}