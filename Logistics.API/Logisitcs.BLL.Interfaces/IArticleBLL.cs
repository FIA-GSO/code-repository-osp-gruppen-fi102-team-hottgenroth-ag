using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;

namespace Logisitcs.BLL.Interfaces
{
    public interface IArticleBll
    {
        IEnumerable<IArticleData> GetAllArticlesByBoxId(string boxId);

        IArticleData GetArticle(string boxId, string articleId);

        bool AddArticle(IArticleData article);

        bool UpdateArticle(IArticleData article);

        bool DeleteArticle(Guid id);
    }
}