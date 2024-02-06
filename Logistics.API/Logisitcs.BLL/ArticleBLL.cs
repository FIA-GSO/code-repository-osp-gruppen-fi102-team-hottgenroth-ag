using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL.Interfaces;
using System;

namespace Logisitcs.BLL
{
    public class ArticleBLL: IArticleBLL
    {
         IArticleDAL _DAL;
         public ArticleBLL(IArticleDAL dal)
         {
            _DAL = dal;
         }

      
    }
}
