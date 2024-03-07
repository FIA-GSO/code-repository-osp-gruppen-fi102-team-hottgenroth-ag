using System;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public class TransportBoxData : ITransportBoxData
    {
        public Guid BoxGuid { get; set; }
        public int? Number { get; set; }
        public string Description { get; set; }
        public string LocationTransport { get; set; }
        public string LocationDeployment { get; set; }
        public string LocationHome { get; set; }
        public string BoxCategory { get; set; }
        public Guid ProjectGuid { get; set; }
    }
}