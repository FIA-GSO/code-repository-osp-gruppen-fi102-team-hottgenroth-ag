﻿using Logisitcs.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.DAL
{
   public static class DbCommandsArticle
   {
      #region Article

      public static IEnumerable<Article> GetAllArticle()
      {
         var db = new LogisticsDbContext();
         return db.Articles.ToList();
      }

      public static IEnumerable<ArticleAndBoxAssignment> GetArticleJoinAssignments(string boxGuid)
      {
         using var db = new LogisticsDbContext();
         IEnumerable<ArticleAndBoxAssignment> result = db.ArticleBoxAssignments
             .Join(
             db.Articles,
             articleBoxAssignment => articleBoxAssignment.ArticleGuid,
             article => article.ArticleGuid,
             (_articleBoxAssignment, _article) => new ArticleAndBoxAssignment(
              _articleBoxAssignment.ArticleGuid,
              _article.ArticleName,
              _article.Description,
              _article.Gtin,
              _article.Unit,
              _articleBoxAssignment.AssignmentGuid,
              _articleBoxAssignment.BoxGuid,
              _articleBoxAssignment.Position,
              _articleBoxAssignment.Status,
              _articleBoxAssignment.Quantity,
              _articleBoxAssignment.ExpiryDate)
             ).ToList();
         result = result.Where(x => x.BoxGuid.ToUpper() == boxGuid.ToUpper()).ToList();
         return result;
      }

      public static void AddArticleAndBoxAssignment(ArticleAndBoxAssignment articleAndBoxAssignment)
      {
         using var db = new LogisticsDbContext();
         Article article = new Article
         {
            ArticleGuid = articleAndBoxAssignment.ArticleGuid,
            ArticleName = articleAndBoxAssignment.ArticleName,
            Description = articleAndBoxAssignment.Description,
            Gtin = articleAndBoxAssignment.Gtin,
            Unit = articleAndBoxAssignment.Unit
         };
         AddArticles(article);
         ArticleBoxAssignment articleBoxAssignment = new ArticleBoxAssignment
         {
            AssignmentGuid = articleAndBoxAssignment.AssignmentGuid,
            ArticleGuid = articleAndBoxAssignment.ArticleGuid,
            BoxGuid = articleAndBoxAssignment.BoxGuid,
            Position = articleAndBoxAssignment.Position,
            Status = articleAndBoxAssignment.Status,
            Quantity = articleAndBoxAssignment.Quantity,
            ExpiryDate = articleAndBoxAssignment.ExpireDate
         };
         DbCommandsArticleBoxAssignment.AddArticleBoxAssignments(articleBoxAssignment);
      }

      public static void UpdateArticleAndBoxAssignment(ArticleAndBoxAssignment articleAndBoxAssignment)
      {
         Article article = new Article
         {
            ArticleGuid = articleAndBoxAssignment.ArticleGuid,
            ArticleName = articleAndBoxAssignment.ArticleName,
            Description = articleAndBoxAssignment.Description,
            Gtin = articleAndBoxAssignment.Gtin,
            Unit = articleAndBoxAssignment.Unit
         };
         UpdateArticle(article);
         ArticleBoxAssignment articleBoxAssignment = new ArticleBoxAssignment
         {
            AssignmentGuid = articleAndBoxAssignment.AssignmentGuid,
            ArticleGuid = articleAndBoxAssignment.ArticleGuid,
            BoxGuid = articleAndBoxAssignment.BoxGuid,
            Position = articleAndBoxAssignment.Position,
            Status = articleAndBoxAssignment.Status,
            Quantity = articleAndBoxAssignment.Quantity,
            ExpiryDate = articleAndBoxAssignment.ExpireDate
         };
         DbCommandsArticleBoxAssignment.UpdateArticleBoxAssignment(articleBoxAssignment);
      }

      public static void DeleteArticleAndBoxAssignment(string articleGuid)
      {
         DbCommandsArticleBoxAssignment.DeleteArticleBoxAssignments(articleGuid);
         DeleteArticles(articleGuid);
      }

      public static ArticleAndBoxAssignment GetArticle(string boxId, string articleId)
      {
         IEnumerable<ArticleAndBoxAssignment> t = GetArticleJoinAssignments(boxId);
         return t.SingleOrDefault(m => m.ArticleGuid == articleId);
      }

      public static void AddArticles(Article article)
      {
         using var db = new LogisticsDbContext();
         db.Articles.Add(article);
         db.SaveChanges();
      }

      public static void DeleteArticles(string guid)
      {
         using var db = new LogisticsDbContext();
         var article = db.Articles.Find(guid);
         if (article == null) return;
         db.Articles.Remove(article);
         db.SaveChanges();
      }

      public static void UpdateArticle(Article article)
      {
         using var db = new LogisticsDbContext();
         db.Articles.Update(article);
         db.SaveChanges();
      }

      #endregion Article
   }
}
