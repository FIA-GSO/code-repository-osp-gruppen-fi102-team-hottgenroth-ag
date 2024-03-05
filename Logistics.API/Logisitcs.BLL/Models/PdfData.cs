using Logisitcs.BLL.Interfaces.ModelInterfaces;

namespace Logisitcs.BLL.Models
{
    public class PdfData
    {
        public ITransportBoxData transportbox { get; set; }
        public IProjectData project { get; set; }
    }
}