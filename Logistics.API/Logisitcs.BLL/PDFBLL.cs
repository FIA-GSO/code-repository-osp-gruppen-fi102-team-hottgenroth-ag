using Logisitcs.BLL.Helper;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class PDFBLL : IPDFBLL
    {
        private readonly PdfHelper pdfHelper;

        public PDFBLL(PdfHelper helper)
        {
            pdfHelper = helper;
        }

        public async Task<byte[]> Create(List<ITransportBoxData> box, IProjectData project, List<IArticleData> articles)
        {
            var result = await pdfHelper.Create(box,project, articles);

            return result;
        }
    }
}