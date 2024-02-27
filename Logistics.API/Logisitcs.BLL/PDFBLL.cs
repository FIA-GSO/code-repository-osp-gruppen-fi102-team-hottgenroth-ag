using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL.Interfaces;

namespace Logisitcs.BLL
{
    public class PDFBLL : IPDFBLL
    {
        private IPDFDAL _DAL;

        public PDFBLL(IPDFDAL dal)
        {
            _DAL = dal;
        }
    }
}