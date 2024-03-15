using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.DAL
{
    public static class DbCommandsArticleBoxAssignment
    {
        public static IEnumerable<ArticleBoxAssignment> GetAllArticleBoxAssignments(string boxId)
        {
            var db = new LogisticsDbContext();
            return db.ArticleBoxAssignments.Where(m => m.BoxGuid == boxId).ToList();
        }

        public static void AddArticleBoxAssignments(ArticleBoxAssignment articleBoxAssignment)
        {
            using var db = new LogisticsDbContext();
            var boxExists = db.Transportboxes.Any(b => b.BoxGuid == articleBoxAssignment.BoxGuid);
            if (!boxExists)
            {
                throw new KeyNotFoundException("BoxGuid does not exist in the Boxes table.");
            }
            var articleExists = db.Articles.Any(a => a.ArticleGuid == articleBoxAssignment.ArticleGuid);
            if (!articleExists)
            {
                throw new KeyNotFoundException("ArticleGuid does not exist in the Articles table.");
            }
            db.ArticleBoxAssignments.Add(articleBoxAssignment);
            db.SaveChanges();
        }

        public static void DeleteArticleBoxAssignments(string articleBoxAssignments)
        {
            using var db = new LogisticsDbContext();
            ArticleBoxAssignment articleBoxAssignment = db.ArticleBoxAssignments.SingleOrDefault(x => x.AssignmentGuid == articleBoxAssignments);
            if (articleBoxAssignment == null) return;
            db.ArticleBoxAssignments.Remove(articleBoxAssignment);
            db.SaveChanges();
        }

        public static void UpdateArticleBoxAssignment(ArticleBoxAssignment articleBoxAssignment)
        {
            using var db = new LogisticsDbContext();
            db.ArticleBoxAssignments.Update(articleBoxAssignment);
            db.SaveChanges();
        }

        public static ArticleBoxAssignment GetArticleBoxAssignment(string guid)
        {
            using var db = new LogisticsDbContext();
            return db.ArticleBoxAssignments.Find(guid);
        }
    }
}