using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logisitcs.DAL;

public record ArticleAndBoxAssignment(
    string ArticleGuid, string ArticleName, string Description, long? Gtin, string Unit, string AssignmentGuid, string BoxGuid, double? Position, long? Status, long? Quantity, string ExpireDate);

public static class DBCommands
{
    #region Article

    public static IEnumerable<Article> GetAllArticle()
    {
        var db = new LogisticsDbContext();
        return db.Articles.ToList();
    }

    public static async Task<IEnumerable<ArticleAndBoxAssignment>> GetArticleJoinAssignments(string boxGuid)
    {
      return await Task.Run(() =>
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
      });
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
        AddArticleBoxAssignments(articleBoxAssignment);
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
        UpdateArticleBoxAssignment(articleBoxAssignment);
    }

    public static void DeleteArticleAndBoxAssignment(string articleGuid)
    {
        DeleteArticleBoxAssignments(articleGuid);
        DeleteArticles(articleGuid);
    }

    public static async Task<ArticleAndBoxAssignment> GetArticle(string boxId, string articleId)
    {
      return await Task.Run(async() =>
      {
         IEnumerable<ArticleAndBoxAssignment> t = await GetArticleJoinAssignments(boxId);
         return t.SingleOrDefault(m => m.ArticleGuid == articleId);
      });
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

    #region Project

    public static IEnumerable<Project> GetAllProjects()
    {
        var db = new LogisticsDbContext();
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
        return db.Projects.Find(guid);
    }

    #endregion Project

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

    public static string GetStatusById(int id)
    {
        using var db = new LogisticsDbContext();
        return db.Statuses.Find(id).Name;
    }

    public static int GetStatusByName(string name)
    {
        using var db = new LogisticsDbContext();
        return int.Parse(db.Statuses.Single(x => x.Name == name).StatusId.ToString());
    }

    #endregion Statues

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

    #region User

    public static void AddUser(User user)
    {
        using var db = new LogisticsDbContext();
        db.Users.Add(user);
        db.SaveChanges();
    }

    public static IEnumerable<User> GetAllUsers()
    {
        var db = new LogisticsDbContext();
        return db.Users.ToList();
    }

    public static void DeleteUser(string guid)
    {
        using var db = new LogisticsDbContext();
        var article = db.Users.Find(guid);
        if (article == null) return;
        db.Users.Remove(article);
        db.SaveChanges();
    }

    public static void UpdateUser(User user)
    {
        using var db = new LogisticsDbContext();
        db.Users.Update(user);
        db.SaveChanges();
    }

    public static User GetUserByMail(string UserMail)
    {
        using var db = new LogisticsDbContext();
        List<User> userList = db.Users.ToList();
        User user = userList.SingleOrDefault(m => m.UserEmail == UserMail);
        return user;
    }

    public static User GetUser(string guid)
    {
        using var db = new LogisticsDbContext();
        return db.Users.Find(guid);
    }

    #endregion User

    #region UserRole

    public static IEnumerable<UserRole> GetAllUserRoles()
    {
        var db = new LogisticsDbContext();
        return db.UserRoles.ToList();
    }

    public static UserRole GetUserRole(int roleId)
    {
        using var db = new LogisticsDbContext();
        return db.UserRoles.Find((long?)roleId);
    }

    public static int GetUserRoleByName(string role)
    {
        using var db = new LogisticsDbContext();
        return int.Parse(db.UserRoles.Single(x => x.Role == role).RoleId.ToString());
    }

    #endregion UserRole
}