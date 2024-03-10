using Logisitcs.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.DAL
{
    public static class DbCommandsTransportBox
    {
        #region Transportboxes

        public static void AddTransportbox(Transportbox transportbox)
        {
            using var db = new LogisticsDbContext();
            db.Transportboxes.Add(transportbox);
            db.SaveChanges();
        }

        public static IEnumerable<Transportbox> GetAllTransportBoxesByPrjGuid(string prjId)
        {
            var db = new LogisticsDbContext();
            return db.Transportboxes.Where(m => m.ProjectGuid == prjId).ToList();
        }

        public static void DeleteTransportbox(string guid)
        {
            using var db = new LogisticsDbContext();
            var article = db.Transportboxes.Find(guid);
            if (article == null) return;
            db.Transportboxes.Remove(article);
            db.SaveChanges();
        }

        public static void UpdateTransportbox(Transportbox transportbox)
        {
            using var db = new LogisticsDbContext();
            db.Transportboxes.Update(transportbox);
            db.SaveChanges();
        }

        public static Transportbox GetTransportbox(string guid)
        {
            using var db = new LogisticsDbContext();
            return db.Transportboxes.Find(guid);
        }

        #endregion Transportboxes
    }
}