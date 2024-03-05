using Logisitcs.BLL.Interfaces;
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
            var t = BLL.GetAllArticlesByBoxId(boxId);
            return Ok(t);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object data)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] object data)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }
    }
}