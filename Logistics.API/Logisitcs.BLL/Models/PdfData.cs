using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System.Collections.Generic;

namespace Logisitcs.BLL.Models
{
    public class PdfData: IPdfData
    {
        public List<ITransportBoxData> Transportbox { get; set; }
        public IProjectData Project { get; set; }
    }
}