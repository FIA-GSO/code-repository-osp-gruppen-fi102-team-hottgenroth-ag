﻿using Logisitcs.DAL.Models;
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
    #endregion

    #region ArticleBoxAssignments
    public static IEnumerable<ArticleBoxAssignment> GetAllTArticleBoxAssignments()
    {
        var db = new LogisticsDbContext();
        return db.ArticleBoxAssignments;
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
    #endregion

    #region Project

    public static IEnumerable<Project> GetAllTProjects()
    {
        var db = new LogisticsDbContext();
        return db.Projects;
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
    #endregion

    #region Statues

    public static IEnumerable<Status> GetAllTransportStatues()
    {
        var db = new LogisticsDbContext();
        return db.Statuses;
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
    #endregion

    #region Transportboxes
    public static void AddTransportbox(Transportbox transportbox)
    {
        using var db = new LogisticsDbContext();
        db.Transportboxes.Add(transportbox);
        db.SaveChanges();
    }

    public static IEnumerable<Transportbox> GetAllTransportBoxes()
    {
        var db = new LogisticsDbContext();
        return db.Transportboxes;
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
    #endregion

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
        return db.Users;
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

    public static User GetUser(string guid)
    {
        using var db = new LogisticsDbContext();
        return db.Users.Find(guid);
    }
    #endregion
}
