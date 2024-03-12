using System.Collections.Generic;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public interface IPdfData
    {
        List<ITransportBoxData> Transportbox { get; set; }
        IProjectData Project { get; set; }
    }
}