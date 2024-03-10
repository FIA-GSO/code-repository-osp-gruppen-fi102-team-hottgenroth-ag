using Logisitcs.BLL.Helper;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
   public class PDFBLL : IPDFBLL
   {
      private readonly PdfHelper pdfHelper;

      public PDFBLL(PdfHelper helper)
      {
         pdfHelper = helper;
      }

      public async Task<byte[]> Create(List<ITransportBoxData> box, IProjectData project)
      {
         if(box != null && project != null)
         {
            IEnumerable<IArticleData> articles = new List<IArticleData>();
            
            foreach(var item in box)
            {
               IEnumerable<IArticleData> boxArticles = await GetAllArticlesByBoxId(item.BoxGuid.ToString());
               if(boxArticles != null && boxArticles.Count() > 0)
               {
                  articles = articles.Concat(boxArticles);
               }
            };

            var result = await pdfHelper.Create(box, project, articles.ToList());

            return result;
         }
         return null;
      }


      private async Task<IEnumerable<IArticleData>> GetAllArticlesByBoxId(string boxId)
      {
         return await Task.Run(() =>
         {
            IEnumerable<ArticleAndBoxAssignment> articleAndBoxAssigments = DbCommandsArticle.GetArticleJoinAssignments(boxId);
            List<IArticleData> articleDatas = new List<IArticleData>();
            foreach (var item in articleAndBoxAssigments)
            {
               string status = string.Empty;
               if (item.Status != null)
               {
                  status = DbCommandsState.GetStatusById((int)item.Status);
               }
               articleDatas.Add(new ArticleData
               {
                  ArticleGuid = item.ArticleGuid,
                  ArticleName = item.ArticleName,
                  Description = item.Description,
                  Gtin = (int?)item.Gtin,
                  ExpiryDate = item.ExpireDate != null && item.ExpireDate != "" ? DateTime.Parse(item.ExpireDate) : null,
                  Quantity = item.Quantity,
                  Position = item.Position,
                  Status = status,
                  Unit = item.Unit,
                  BoxGuid = Guid.Parse(item.BoxGuid),
                  ArticleBoxAssignment = Guid.Parse(item.AssignmentGuid)
               });
            }
            return articleDatas;
         });
      }
   }
}