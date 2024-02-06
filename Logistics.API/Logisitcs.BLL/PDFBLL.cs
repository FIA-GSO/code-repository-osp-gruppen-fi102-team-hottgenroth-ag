using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL.Interfaces;
using System;

namespace Logisitcs.BLL
{
    public class PDFBLL: IPDFBLL
    {
         IPDFDAL _DAL;
         public PDFBLL(IPDFDAL dal)
         {
            _DAL = dal;
         }

      
    }
}
