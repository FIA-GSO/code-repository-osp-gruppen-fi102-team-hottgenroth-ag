using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
    public interface IPDFBLL
    {
        Task<byte[]> Create(List<ITransportBoxData> data);
    }
}