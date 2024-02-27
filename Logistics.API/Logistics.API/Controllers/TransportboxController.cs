using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Logistics.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportboxController : ControllerBase
    {
        private readonly ITransportboxBll transportboxBll;

        public TransportboxController(ITransportboxBll bll)
        {
            transportboxBll = bll;
        }

        [HttpGet("all/{prjId}")]
        public async Task<IActionResult> GetAll(string prjId)
        {
            var result = await transportboxBll.GetAllTransportBoxesByPrjGuid(prjId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await transportboxBll.GetTransportbox(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ITransportBoxData data)
        {
            var result = await transportboxBll.AddTransportbox(data);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ITransportBoxData data)
        {
            var result = await transportboxBll.UpdateTransportbox(data);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await transportboxBll.DeleteTransportbox(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}