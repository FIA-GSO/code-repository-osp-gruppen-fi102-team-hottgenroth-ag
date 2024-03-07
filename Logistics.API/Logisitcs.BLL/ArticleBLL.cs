using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using Logisitcs.DAL.Interfaces;
using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class ArticleBll : IArticleBll
    {
        public ArticleBll(IArticleDAL dal)
        {
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
                articleDatas.Add(new ArticleData
                {
                    ArticleGuid = item.ArticleGuid,
                    ArticleName = item.ArticleName,
                    Description = item.Description,
                    Gtin = (int?)item.Gtin,
                    ExpiryDate = item.ExpireDate != null && item.ExpireDate != "" ? DateTime.Parse(item.ExpireDate) : null,
                    Quantity = (int?)item.Quantity,
                    Position = item.Position,
                    Status = status,
                    Unit = item.Unit,
                    BoxGuid = Guid.Parse(item.BoxGuid),
                });
            }
            return articleDatas;
         });
        }

        public async Task<IArticleData> GetArticle(string boxId, string articleId)
        {
         return await Task.Run(async() =>
         {

            ArticleAndBoxAssignment articleAndBoxAssignment = await DBCommands.GetArticle(boxId, articleId);
            string status = string.Empty;
            if (articleAndBoxAssignment.Status != null)
            {
               status = DBCommands.GetStatusById((int)articleAndBoxAssignment.Status);
            }
            IArticleData articelData = new ArticleData
            {
               ArticleGuid = articleAndBoxAssignment.ArticleGuid,
               ArticleName = articleAndBoxAssignment.ArticleName,
               Description = articleAndBoxAssignment.Description,
               Gtin = (int?)articleAndBoxAssignment.Gtin,
               ExpiryDate = articleAndBoxAssignment.ExpireDate != null ? DateTime.Parse(articleAndBoxAssignment.ExpireDate) : null,
               Quantity = (int?)articleAndBoxAssignment.Quantity,
               Position = articleAndBoxAssignment.Position,
               Status = status,
               Unit = articleAndBoxAssignment.Unit,
               BoxGuid = Guid.Parse(articleAndBoxAssignment.BoxGuid),
            };
            return articelData;
         });
        }

        public bool AddArticle(IArticleData article)
        {
            ArticleAndBoxAssignment articleAndBoxAssignment = new ArticleAndBoxAssignment
            (Guid.NewGuid().ToString(),
             article.ArticleName,
             article.Description,
             article.Gtin,
             article.Unit,
             Guid.NewGuid().ToString(),
             article.BoxGuid.ToString(),
             article.Position,
             DBCommands.GetStatusByName(article.Status),
             article.Quantity,
             article.ExpiryDate.ToString());
            try
            {
                DBCommands.AddArticleAndBoxAssignment(articleAndBoxAssignment);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateArticle(IArticleData article)
        {
            ArticleAndBoxAssignment articleAndBoxAssignment = new ArticleAndBoxAssignment
            (article.ArticleGuid.ToString(),
             article.ArticleName,
             article.Description,
             article.Gtin,
             article.Unit,
             article.ArticleBoxAssignment.ToString(),
             article.BoxGuid.ToString(),
             article.Position,
             DBCommands.GetStatusByName(article.Status),
             article.Quantity,
             article.ExpiryDate.ToString());
            try
            {
                DBCommands.UpdateArticleAndBoxAssignment(articleAndBoxAssignment);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteArticle(Guid id)
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
        }
    }
}