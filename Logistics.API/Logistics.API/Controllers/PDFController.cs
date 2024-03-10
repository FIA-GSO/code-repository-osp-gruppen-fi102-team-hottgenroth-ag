using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PDFController : ControllerBase
    {
        protected IPDFBLL BLL { get; }

        public PDFController(IPDFBLL bll)
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
        public async Task<IActionResult> Create([FromBody] PDF_Data data)
        {
            var result = await BLL.Create(data.transportbox, data.project, data.articles);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
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

public struct PDF_Data
{
    public List<ITransportBoxData> transportbox { get; set; }
    public IProjectData project { get; set; }
    public List<IArticleData> articles { get; set; }
}