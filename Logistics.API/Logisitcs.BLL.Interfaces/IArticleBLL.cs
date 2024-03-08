using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
    public interface IArticleBll
    {
        Task<IEnumerable<IArticleData>> GetAllArticlesByBoxId(string boxId);

      Task<IArticleData> GetArticle(string boxId, string articleId);

        bool AddArticle(IArticleData article);

        bool UpdateArticle(IArticleData article);

        bool DeleteArticle(Guid id);
    }
}