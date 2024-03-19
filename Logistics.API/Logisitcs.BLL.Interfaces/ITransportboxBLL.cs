using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
    public interface ITransportboxBll
    {
        Task<IEnumerable<ITransportBoxData>> GetAllTransportBoxesByProjectGuid(string projectGuid);

        Task<IEnumerable<ITransportBoxData>> GetAllTransportBoxesWithoutPrjGuid();

        Task<ITransportBoxData> GetTransportbox(Guid transportboxGuid);

        Task<ITransportBoxData> AddTransportbox(ITransportBoxData transportBoxData);

        Task<bool> UpdateTransportbox(ITransportBoxData transportBoxData);

        Task<bool> DeleteTransportbox(Guid transportboxGuid);
    }
}