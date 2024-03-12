using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        protected IArticleBll articleBll { get; }

        public ArticleController(IArticleBll articleBll)
        {
            this.articleBll = articleBll;
        }

        [HttpGet("all/{boxId}")]
        public async Task<IActionResult> GetAll(string boxId)
        {
            IEnumerable<IArticleData> result = await articleBll.GetAllArticlesByBoxId(boxId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

         [HttpGet("all")]
         public async Task<IActionResult> GetAll()
         {
            IEnumerable<IArticleData> result = await articleBll.GetAllArticles();
            if (result == null)
            {
               return NotFound();
            }
            return Ok(result);
         }

      [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid boxId, Guid articleId)
        {
            IArticleData result = await articleBll.GetArticle(boxId.ToString(), articleId.ToString());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IArticleData data)
        {
            var result = await articleBll.AddArticle(data);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IArticleData data)
        {
            var result = await articleBll.UpdateArticle(data);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await articleBll.DeleteArticle(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}