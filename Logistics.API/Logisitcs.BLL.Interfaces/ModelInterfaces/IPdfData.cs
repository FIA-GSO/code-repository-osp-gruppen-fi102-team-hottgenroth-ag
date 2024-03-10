using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public interface IPdfData
    {
        ITransportBoxData transportbox { get; set; }
        IProjectData project { get; set; }
    }
}