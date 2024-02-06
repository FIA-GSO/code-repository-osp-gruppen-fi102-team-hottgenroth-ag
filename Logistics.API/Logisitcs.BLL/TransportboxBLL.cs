using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL.Interfaces;
using System;

namespace Logisitcs.BLL
{
    public class TransportboxBLL: ITransportboxBLL
   {
         ITransportboxDAL _DAL;
         public TransportboxBLL(ITransportboxDAL dal)
         {
            _DAL = dal;
         }
      
    }
}
