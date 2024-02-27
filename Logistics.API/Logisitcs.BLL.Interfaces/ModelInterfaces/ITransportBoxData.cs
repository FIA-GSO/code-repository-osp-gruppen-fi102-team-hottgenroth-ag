using System;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public interface ITransportBoxData
    {
        Guid BoxGuid { get; set; }
        int Number { get; set; }
        string Description { get; set; }
        string LocationTransport { get; set; }
        string LocationDeployment { get; set; }
        string LocationHome { get; set; }
        string BoxCategory { get; set; }
    }
}