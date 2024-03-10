using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;

namespace Logisitcs.BLL.Interfaces.Factories
{
    public interface IArticleDataFactory
    {
        IArticleData Create(ArticleAndBoxAssignment articleAndBoxAssignment);
    }
}