using Logisitcs.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.DAL
{
    public static class DbCommandsProject
    {
        #region Project

        public static IEnumerable<Project> GetAllProjects()
        {
            using var db = new LogisticsDbContext();
            return db.Projects.ToList();
        }

        public static void AddProject(Project project)
        {
            using var db = new LogisticsDbContext();
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public static void DeleteProject(string guid)
        {
            using var db = new LogisticsDbContext();
            var article = db.Projects.Find(guid);
            if (article == null) return;
            db.Projects.Remove(article);
            db.SaveChanges();
        }

        public static void UpdateProject(Project project)
        {
            using var db = new LogisticsDbContext();
            db.Projects.Update(project);
            db.SaveChanges();
        }

        public static Project GetProject(string guid)
        {
            using var db = new LogisticsDbContext();
            return db.Projects.FirstOrDefault(x => x.ProjectGuid == guid);
        }

        #endregion Project
    }
}