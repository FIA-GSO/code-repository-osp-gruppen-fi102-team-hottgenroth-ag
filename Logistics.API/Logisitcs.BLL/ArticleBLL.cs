using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL.Interfaces;

namespace Logisitcs.BLL
{
    public class ArticleBll : IArticleBll
    {
        private IArticleDAL _DAL;

        public ArticleBll(IArticleDAL dal)
        {
            _DAL = dal;
        }
    }
}