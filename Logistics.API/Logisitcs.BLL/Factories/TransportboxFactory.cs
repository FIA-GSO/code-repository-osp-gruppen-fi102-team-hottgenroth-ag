using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Models;

namespace Logisitcs.BLL.Factories
{
    public class TransportboxFactory : ITransportboxFactory
    {
        public Transportbox Create(ITransportBoxData transportBoxData)
        {
            return new Transportbox
            {
                BoxGuid = transportBoxData.BoxGuid.ToString(),
                Number = transportBoxData.Number,
                Description = transportBoxData.Description,
                LocationTransport = transportBoxData.LocationTransport,
                LocationDeployment = transportBoxData.LocationDeployment,
                LocationHome = transportBoxData.LocationHome,
                BoxCategory = transportBoxData.BoxCategory,
            };
        }
    }
}