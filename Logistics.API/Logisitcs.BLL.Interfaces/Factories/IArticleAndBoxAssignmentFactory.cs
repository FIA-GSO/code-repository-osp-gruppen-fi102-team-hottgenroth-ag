using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;

namespace Logisitcs.BLL.Interfaces.Factories
{
    public interface IArticleAndBoxAssignmentFactory
    {
        ArticleAndBoxAssignment CreateAdd(IArticleData articleData);

        ArticleAndBoxAssignment CreateUpdate(IArticleData articleData);
    }
}