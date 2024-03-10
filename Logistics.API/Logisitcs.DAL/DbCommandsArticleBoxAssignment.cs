using Logisitcs.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.DAL
{
    public static class DbCommandsArticleBoxAssignment
    {
        #region ArticleBoxAssignments

        public static IEnumerable<ArticleBoxAssignment> GetAllArticleBoxAssignments(string boxId)
        {
            var db = new LogisticsDbContext();
            return db.ArticleBoxAssignments.Where(m => m.BoxGuid == boxId).ToList();
        }

        public static void AddArticleBoxAssignments(ArticleBoxAssignment articleBoxAssignment)
        {
            using var db = new LogisticsDbContext();
            db.ArticleBoxAssignments.Add(articleBoxAssignment);
            db.SaveChanges();
        }

        public static void DeleteArticleBoxAssignments(string guid)
        {
            using var db = new LogisticsDbContext();
            ArticleBoxAssignment article = db.ArticleBoxAssignments.SingleOrDefault(x => x.ArticleGuid == guid);
            if (article == null) return;
            db.ArticleBoxAssignments.Remove(article);
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

        #endregion ArticleBoxAssignments
    }
}