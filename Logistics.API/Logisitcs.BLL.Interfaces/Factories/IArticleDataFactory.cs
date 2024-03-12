using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using Logisitcs.DAL.Models;

namespace Logisitcs.BLL.Interfaces.Factories
{
    public interface IArticleDataFactory
    {
        IArticleData Create(ArticleAndBoxAssignment articleAndBoxAssignment);
        IArticleData Create(Article article);
    }
}