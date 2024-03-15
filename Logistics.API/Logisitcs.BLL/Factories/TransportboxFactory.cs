using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Models;
using System;

namespace Logisitcs.BLL.Factories
{
    public class TransportboxFactory : ITransportboxFactory
    {
        public Transportbox Create(ITransportBoxData transportBoxData)
        {
            var prjGuid = transportBoxData.ProjectGuid != Guid.Empty ? transportBoxData.ProjectGuid.ToString() : null;
            return new Transportbox
            {
                BoxGuid = transportBoxData.BoxGuid.ToString(),
                Number = transportBoxData.Number,
                Description = transportBoxData.Description,
                LocationTransport = transportBoxData.LocationTransport,
                LocationDeployment = transportBoxData.LocationDeployment,
                LocationHome = transportBoxData.LocationHome,
                BoxCategory = transportBoxData.BoxCategory,
                ProjectGuid = prjGuid
            };
        }
    }
}