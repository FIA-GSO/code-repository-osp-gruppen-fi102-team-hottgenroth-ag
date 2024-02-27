using Logisitcs.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.DAL;

public static class DBCommands
{
    #region Article

    public static IEnumerable<Article> GetAllArticle()
    {
        var db = new LogisticsDbContext();
        return db.Articles.ToList();
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

    public static Article GetArticle(string guid)
    {
        using var db = new LogisticsDbContext();
        return db.Articles.Find(guid);
    }

    #endregion Article

    #region ArticleBoxAssignments

    public static IEnumerable<ArticleBoxAssignment> GetAllTArticleBoxAssignments()
    {
        var db = new LogisticsDbContext();
        return db.ArticleBoxAssignments.ToList();
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
        var article = db.ArticleBoxAssignments.Find(guid);
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

    public static Status GetStatus(string guid)
    {
        using var db = new LogisticsDbContext();
        return db.Statuses.Find(guid);
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