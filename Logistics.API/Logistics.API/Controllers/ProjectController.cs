using Logisitcs.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class ProjectController : ControllerBase
   {
      protected IProjectBLL BLL { get; }

      public ProjectController(IProjectBLL bll)
      {
         BLL = bll;
      }

      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         return Ok();
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
