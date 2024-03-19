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
    public class TransportboxBll : ITransportboxBll
    {
        private readonly ITransportBoxDataFactory transportBoxDataFactory;
        private readonly ITransportboxFactory transportboxFactory;

        public TransportboxBll(ITransportBoxDataFactory transportBoxDataFactory, ITransportboxFactory transportboxFactory)
        {
            this.transportBoxDataFactory = transportBoxDataFactory;
            this.transportboxFactory = transportboxFactory;
        }

        //Alle Transportboxen holen die einem bestimmten Projekt zugeordnet sind
        public async Task<IEnumerable<ITransportBoxData>> GetAllTransportBoxesByProjectGuid(string projectGuid)
        {
            return await Task.Run(() =>
            {
                IEnumerable<Transportbox> transportBox = DbCommandsTransportBox.GetAllTransportBoxesByPrjGuid(projectGuid);
                IEnumerable<ITransportBoxData> projectDatas = transportBox.Select(x => transportBoxDataFactory.Create(x));
                return projectDatas;
            });
        }

        //Alle Transportboxen holen die keinem Projekt zugeordnet sind
        public async Task<IEnumerable<ITransportBoxData>> GetAllTransportBoxesWithoutPrjGuid()
        {
            return await Task.Run(() =>
            {
                IEnumerable<Transportbox> transportBox = DbCommandsTransportBox.GetAllTransportBoxesWithoutPrjGuid();
                IEnumerable<ITransportBoxData> boxData = transportBox.Select(x => transportBoxDataFactory.Create(x));
                return boxData;
            });
        }

        //Eine bestimmte Transportbox holen
        public async Task<ITransportBoxData> GetTransportbox(Guid transportboxGuid)
        {
            return await Task.Run(() =>
            {
                Transportbox transportbox = DbCommandsTransportBox.GetTransportbox(transportboxGuid.ToString());
                //If no box is found, return null
                if (transportbox != null)
                {
                    ITransportBoxData transportboxData = transportBoxDataFactory.Create(transportbox);
                    return transportboxData;
                }
                return null;
            });
        }

        //Eine Transportbox der Datenbank hinzufügen
        public async Task<ITransportBoxData> AddTransportbox(ITransportBoxData transportBoxData)
        {
            return await Task.Run(() =>
            {
                Transportbox transportbox = transportboxFactory.Create(transportBoxData);
                DbCommandsTransportBox.AddTransportbox(transportbox);
                ITransportBoxData transportBoxDataDb = transportBoxDataFactory.Create(transportbox);
                return transportBoxDataDb;
            });
        }

        //Eine bestimmte Transportbox verändern(update)
        public async Task<bool> UpdateTransportbox(ITransportBoxData transportBoxData)
        {
            return await Task.Run(() =>
            {
                var dbProject = DbCommandsTransportBox.GetTransportbox(transportBoxData.BoxGuid.ToString());
                //Wenn keine Transportbox gefunden wird false ausgeben
                if (dbProject == null)
                {
                    return false;
                }
                Transportbox transportbox = transportboxFactory.Create(transportBoxData);
                DbCommandsTransportBox.UpdateTransportbox(transportbox);
                //Wenn Transportbox erfolgreich geupdatet true ausgeben
                return true;
            });
        }

        //Eine bestimmte Transportbox löschen
        public async Task<bool> DeleteTransportbox(Guid transportboxGuid)
        {
            return await Task.Run(() =>
            {
                Transportbox transportbox = DbCommandsTransportBox.GetTransportbox(transportboxGuid.ToString());
                //Wenn keine Transportbox gefunden wird false ausgeben
                if (transportbox == null)
                {
                    return false;
                }
                DbCommandsTransportBox.DeleteTransportbox(transportboxGuid.ToString());
                //Wenn Transportbox erfolgreich gelöscht true ausgeben
                return true;
            });
        }
    }
}