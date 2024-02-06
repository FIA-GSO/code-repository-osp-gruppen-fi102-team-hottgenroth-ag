using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL.Interfaces;
using System;

namespace Logisitcs.BLL
{
    public class GoodsBLL: IGoodsBLL
    {
         IGoodsDAL _DAL;
         public GoodsBLL(IGoodsDAL dal)
         {
            _DAL = dal;
         }

      
    }
}
