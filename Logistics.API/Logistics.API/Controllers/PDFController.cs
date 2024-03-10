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

   

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IPdfData data)
        {
            var result = await BLL.Create(data.Transportbox, data.Project);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
