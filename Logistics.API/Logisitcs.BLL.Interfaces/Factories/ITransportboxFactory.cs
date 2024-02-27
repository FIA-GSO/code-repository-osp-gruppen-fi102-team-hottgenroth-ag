using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Models;

namespace Logisitcs.BLL.Interfaces.Factories
{
    public interface ITransportboxFactory
    {
        Transportbox Create(ITransportBoxData transportBoxData);
    }
}