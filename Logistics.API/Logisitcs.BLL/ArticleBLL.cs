using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
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

        public async Task<IEnumerable<IArticleData>> GetAllArticlesByBoxId(string boxId)
        {
            return await Task.Run(() =>
            {
                IEnumerable<ArticleAndBoxAssignment> articleAndBoxAssigments = DBCommands.GetArticleJoinAssignments(boxId);
                List<IArticleData> articleDatas = new List<IArticleData>();
                foreach (var item in articleAndBoxAssigments)
                {
                    string status = string.Empty;
                    if (item.Status != null)
                    {
                        status = DBCommands.GetStatusById((int)item.Status);
                    }
                    articleDatas.Add(articleDataFactory.Create(item));
                }
                return articleDatas;
            });
        }

        public async Task<IArticleData> GetArticle(string boxId, string articleId)
        {
            return await Task.Run(async () =>
            {
                ArticleAndBoxAssignment articleAndBoxAssignment = DBCommands.GetArticle(boxId, articleId);
                string status = string.Empty;
                if (articleAndBoxAssignment.Status != null)
                {
                    status = DBCommands.GetStatusById(articleAndBoxAssignment.Status);
                }
                IArticleData articelData = articleDataFactory.Create(articleAndBoxAssignment);
                return articelData;
            });
        }

        public async Task<bool> AddArticle(IArticleData article)
        {
            return await Task.Run(async () =>
            {
                ArticleAndBoxAssignment articleAndBoxAssignment = articleAndBoxAssignmentFactory.CreateAdd(article);
                try
                {
                    DBCommands.AddArticleAndBoxAssignment(articleAndBoxAssignment);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateArticle(IArticleData article)
        {
            return await Task.Run(async () =>
            {
                ArticleAndBoxAssignment articleAndBoxAssignment = articleAndBoxAssignmentFactory.CreateUpdate(article);
                try
                {
                    DBCommands.UpdateArticleAndBoxAssignment(articleAndBoxAssignment);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteArticle(Guid id)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DBCommands.DeleteArticleAndBoxAssignment(id.ToString());
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