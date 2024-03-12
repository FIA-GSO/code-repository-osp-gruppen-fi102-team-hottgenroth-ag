using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class TransportboxBLL : ITransportboxBll
    {
        private readonly ITransportBoxDataFactory transportDataFactory;
        private readonly ITransportboxFactory transportboxFactory;

        public TransportboxBLL(ITransportBoxDataFactory dataFactory, ITransportboxFactory boxFactory)
        {
            transportDataFactory = dataFactory;
            transportboxFactory = boxFactory;
        }

        public async Task<IEnumerable<ITransportBoxData>> GetAllTransportBoxesByPrjGuid(string prj)
        {
            return await Task.Run(() =>
            {
                IEnumerable<Transportbox> transportBox = DbCommandsTransportBox.GetAllTransportBoxesByPrjGuid(prj);
                IEnumerable<ITransportBoxData> projectDatas = transportBox.Select(x => transportDataFactory.Create(x));
                return projectDatas;
            });
        }

        public async Task<IEnumerable<ITransportBoxData>> GetAllTransportBoxesWithoutPrjGuid()
        {
           return await Task.Run(() =>
           {
              IEnumerable<Transportbox> transportBox = DbCommandsTransportBox.GetAllTransportBoxesWithoutPrjGuid();
              IEnumerable<ITransportBoxData> boxData = transportBox.Select(x => transportDataFactory.Create(x));
              return boxData;
           });
        }

        public async Task<ITransportBoxData> GetTransportbox(Guid guid)
        {
            return await Task.Run(() =>
            {
                Transportbox transportbox = DbCommandsTransportBox.GetTransportbox(guid.ToString());
                //If no box is found, return null
                if (transportbox != null)
                {
                    ITransportBoxData transportboxData = transportDataFactory.Create(transportbox);
                    return transportboxData;
                }
                return null;
            });
        }

        public async Task<ITransportBoxData> AddTransportbox(ITransportBoxData transportBoxData)
        {
            return await Task.Run(() =>
            {
                Transportbox transportbox = transportboxFactory.Create(transportBoxData);
                DbCommandsTransportBox.AddTransportbox(transportbox);
                ITransportBoxData transportBoxDataDb = transportDataFactory.Create(transportbox);
                return transportBoxDataDb;
            });
        }

        public async Task<bool> UpdateTransportbox(ITransportBoxData transportBoxData)
        {
            return await Task.Run(() =>
            {
                var dbProject = DbCommandsTransportBox.GetTransportbox(transportBoxData.BoxGuid.ToString());
                if (dbProject == null)
                {
                    return false;
                }
                Transportbox transportbox = transportboxFactory.Create(transportBoxData);
                DbCommandsTransportBox.UpdateTransportbox(transportbox);
                return true;
            });
        }

        public async Task<bool> DeleteTransportbox(Guid guid)
        {
            return await Task.Run(() =>
            {
                Transportbox transportbox = DbCommandsTransportBox.GetTransportbox(guid.ToString());
                if (transportbox == null)
                {
                    return false;
                }
                DbCommandsTransportBox.DeleteTransportbox(guid.ToString());
                return true;
            });
        }
    }
}