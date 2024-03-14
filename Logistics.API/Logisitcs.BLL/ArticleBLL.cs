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

        public async Task<IEnumerable<IArticleData>> GetAllArticles()
        {
            return await Task.Run(() =>
            {
                IEnumerable<Article> articles = DbCommandsArticle.GetAllArticle();
                List<IArticleData> articleDatas = new List<IArticleData>();
                foreach (var item in articles)
                {
                    articleDatas.Add(articleDataFactory.Create(item));
                }
                return articleDatas;
            });
        }

        public async Task<IEnumerable<IArticleData>> GetAllArticlesByBoxId(string boxGuid)
        {
            return await Task.Run(() =>
            {
                IEnumerable<ArticleAndBoxAssignment> articleAndBoxAssigments = DbCommandsArticle.GetArticleJoinAssignments(boxGuid);
                List<IArticleData> articleDatas = new List<IArticleData>();
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

        public async Task<IArticleData> GetArticle(string boxGuid, string assignmentGuid)
        {
            return await Task.Run(() =>
            {
                ArticleAndBoxAssignment articleAndBoxAssignment = DbCommandsArticle.GetArticle(boxGuid, assignmentGuid);
                string status = string.Empty;
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
                ArticleAndBoxAssignment articleAndBoxAssignment = articleAndBoxAssignmentFactory.CreateAdd(article);
                try
                {
                    DbCommandsArticle.AddArticleAndBoxAssignment(articleAndBoxAssignment);
                    return article;
                }
                catch
                {
                    return null;
                }
            });
        }

        public async Task<bool> UpdateArticle(IArticleData article)
        {
            return await Task.Run(() =>
            {
                ArticleAndBoxAssignment articleAndBoxAssignment = articleAndBoxAssignmentFactory.CreateUpdate(article);
                try
                {
                    DbCommandsArticle.UpdateArticleAndBoxAssignment(articleAndBoxAssignment);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteArticle(Guid articleBoxAssignmentsGuid)
        {
            return await Task.Run(() =>
            {
                try
                {
                    DbCommandsArticleBoxAssignment.DeleteArticleBoxAssignments(articleBoxAssignmentsGuid.ToString());
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}