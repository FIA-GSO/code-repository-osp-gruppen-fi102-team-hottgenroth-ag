using Logisitcs.BLL.Helper;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class PDFBLL : IPDFBLL
    {
        private IPDFDAL _DAL;
        private PDFHelper _pdfHelper;

        public PDFBLL(IPDFDAL dal)
        {
            _DAL = dal;
            _pdfHelper = new PDFHelper();
        }

        public async Task<byte[]> Create(List<ITransportBoxData> box, IProjectData project, List<IArticleData> articles)
        {
            var result = await _pdfHelper.Create(box,project, articles);

            return result;
        }
    }
}