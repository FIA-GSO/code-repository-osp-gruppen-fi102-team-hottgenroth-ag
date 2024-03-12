using Logisitcs.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.DAL
{
    public static class DbCommandsState
    {
        #region Statues

        public static IEnumerable<Status> GetAllStatues()
        {
            var db = new LogisticsDbContext();
            return db.Statuses.ToList();
        }

        public static void AddStatus(Status status)
        {
            using var db = new LogisticsDbContext();
            db.Statuses.Add(status);
            db.SaveChanges();
        }

        public static void DeleteStatus(string guid)
        {
            using var db = new LogisticsDbContext();
            var article = db.Statuses.Find(guid);
            if (article == null) return;
            db.Statuses.Remove(article);
            db.SaveChanges();
        }

        public static void UpdateStatus(Status status)
        {
            using var db = new LogisticsDbContext();
            db.Statuses.Update(status);
            db.SaveChanges();
        }

        public static string GetStatusById(int? id)
        {
            using var db = new LogisticsDbContext();
            return db.Statuses.Find(id).Name;
        }

        public static int GetStatusByName(string name)
        {
            using var db = new LogisticsDbContext();
            return db.Statuses.Single(x => x.Name == name).StatusId;
        }

        #endregion Statues
    }
}