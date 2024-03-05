using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        protected IArticleBll BLL { get; }

        public ArticleController(IArticleBll bll)
        {
            BLL = bll;
        }

        [HttpGet("all/{boxId}")]
        public async Task<IActionResult> GetAll(string boxId)
        {
            IEnumerable<IArticleData> result = BLL.GetAllArticlesByBoxId(boxId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid boxId, Guid articleId)
        {
            IArticleData result = BLL.GetArticle(boxId.ToString(), articleId.ToString());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IArticleData data)
        {
            var result = BLL.AddArticle(data);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IArticleData data)
        {
            var result = BLL.UpdateArticle(data);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = BLL.DeleteArticle(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}