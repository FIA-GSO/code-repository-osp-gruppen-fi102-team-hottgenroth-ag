using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Models;
using System;

namespace Logisitcs.BLL.Factories
{
    public class TransportBoxDataFactory : ITransportBoxDataFactory
    {
        public ITransportBoxData Create(Transportbox transportbox)
        {
            return new TransportBoxData
            {
                BoxGuid = Guid.Parse(transportbox.BoxGuid),
                Number = transportbox.Number,
                Description = transportbox.Description,
                LocationTransport = transportbox.LocationTransport,
                LocationDeployment = transportbox.LocationDeployment,
                LocationHome = transportbox.LocationHome,
                BoxCategory = transportbox.BoxCategory,
                ProjectGuid = Guid.Parse(transportbox.ProjectGuid),
            };
        }
    }
}