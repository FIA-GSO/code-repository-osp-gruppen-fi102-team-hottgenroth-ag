using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class ArticleBll : IArticleBll
    {
        private readonly IArticleAndBoxAssignmentFactory articleAndBoxAssignmentFactory;
        private readonly IArticleDataFactory articleDataFactory;

        public ArticleBll(IArticleAndBoxAssignmentFactory articleAndBoxAssignmentFactory, IArticleDataFactory articleDataFactory)
        {
            this.articleAndBoxAssignmentFactory = articleAndBoxAssignmentFactory;
            this.articleDataFactory = articleDataFactory;
        }

        //Gibt alle Artikel zurück
        public async Task<IEnumerable<IArticleData>> GetAllArticles()
        {
            return await Task.Run(() =>
            {
                IEnumerable<Article> articles = DbCommandsArticle.GetAllArticle();
                //Fügt alle Artikel zu einer Liste von IArticleData hinzu
                List<IArticleData> articleDatas = new();
                foreach (var item in articles)
                {
                    articleDatas.Add(articleDataFactory.Create(item));
                }
                return articleDatas;
            });
        }

        //Gibt alle Artikel zurück die zu einer Box gehören
        public async Task<IEnumerable<IArticleData>> GetAllArticlesByBoxId(string boxGuid)
        {
            return await Task.Run(() =>
            {
                IEnumerable<ArticleAndBoxAssignment> articleAndBoxAssigments = DbCommandsArticle.GetArticleJoinAssignments(boxGuid);
                List<IArticleData> articleDatas = new();
                //Fügt alle Artikel der Box zu einer Liste von IArticleData hinzu
                foreach (var item in articleAndBoxAssigments)
                {
                    string status = string.Empty;
                    if (item.Status != null)
                    {
                        status = DbCommandsState.GetStatusById((int)item.Status);
                    }
                    articleDatas.Add(articleDataFactory.Create(item));
                }
                return articleDatas;
            });
        }

        //Gibt einen Artikel zurück anhand der Box und dem Assignment
        public async Task<IArticleData> GetArticle(string boxGuid, string assignmentGuid)
        {
            return await Task.Run(() =>
            {
                //Gibt das Assignment zurück
                ArticleAndBoxAssignment articleAndBoxAssignment = DbCommandsArticle.GetArticle(boxGuid, assignmentGuid);
                string status = string.Empty;
                //Wenn das Assignment nicht null ist und ein Status hat
                if (articleAndBoxAssignment != null && articleAndBoxAssignment.Status != null)
                {
                    status = DbCommandsState.GetStatusById(articleAndBoxAssignment.Status);
                    IArticleData articelData = articleDataFactory.Create(articleAndBoxAssignment);
                    return articelData;
                }
                return null;
            });
        }

        public async Task<IArticleData> AddArticle(IArticleData article)
        {
            return await Task.Run(() =>
            {
                //Erstellt ein neues Assignment
                ArticleAndBoxAssignment articleAndBoxAssignment = articleAndBoxAssignmentFactory.CreateAdd(article);
                try
                {
                    //Fügt das Assignment hinzu und gibt das Assignment zurück
                    DbCommandsArticle.AddArticleAndBoxAssignment(articleAndBoxAssignment);
                    return article;
                }
                catch
                {
                    //Wenn ein Fehler auftritt wird null zurückgegeben
                    return null;
                }
            });
        }

        public async Task<bool> UpdateArticle(IArticleData article)
        {
            return await Task.Run(() =>
            {
                //Updated ein neues Assignment
                ArticleAndBoxAssignment articleAndBoxAssignment = articleAndBoxAssignmentFactory.CreateUpdate(article);
                try
                {
                    //Updated das Assignment und gibt true zurück
                    DbCommandsArticle.UpdateArticleAndBoxAssignment(articleAndBoxAssignment);
                    return true;
                }
                catch
                {
                    //Wenn ein Fehler gecatched wird, wird false zurückgegeben
                    return false;
                }
            });
        }

        public async Task<bool> DeleteArticle(Guid articleBoxAssignmentsGuid)
        {
            return await Task.Run(() =>
            {
                //Wenn das Assignment nicht in der DB ist, wird false zurückgegeben
                if (DbCommandsArticleBoxAssignment.GetArticleBoxAssignment(articleBoxAssignmentsGuid.ToString()) == null)
                {
                    return false;
                }
                //Löscht das Assignment und gibt true zurück
                DbCommandsArticleBoxAssignment.DeleteArticleBoxAssignments(articleBoxAssignmentsGuid.ToString());
                return true;
            });
        }
    }
}