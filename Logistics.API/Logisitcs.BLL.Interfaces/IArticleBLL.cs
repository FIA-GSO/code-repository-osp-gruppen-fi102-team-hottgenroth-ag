using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
    public interface IArticleBll
    {
        Task<IEnumerable<IArticleData>> GetAllArticlesByBoxId(string boxGuid);

        Task<IEnumerable<IArticleData>> GetAllArticles();

        Task<IArticleData> GetArticle(string boxGuid, string assignmentGuid);

        Task<IArticleData> AddArticle(IArticleData article);

        Task<bool> UpdateArticle(IArticleData article);

        Task<bool> DeleteArticle(Guid articleBoxAssignmentsGuid);
    }
}