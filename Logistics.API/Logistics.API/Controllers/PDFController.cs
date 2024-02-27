using Logisitcs.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
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
        public async Task<IActionResult> Create([FromBody] object data)
        {
            var result = await BLL.Create(data);

            if (result == null)
            {
                return NotFound();
            }

            // use name of created document if we add guid,...
            // data.pdfName = result;

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

//public struct PDF_Data
//{
//    public Dictionary<string, string> replacementValues { get; set; }
//    public string pdfName { get; set; }
//}

//public struct PDF_Notes_Images_Data
//{
//    public IWAppContact UserAddress { get; set; }
//    public int LangValue { get; set; }
//    public bool PrintImages { get; set; }
//    public bool PrintNotes { get; set; }
//    public List<IMediaData> StoreImages { get; set; }
//    public List<INoteData> StoreNotes { get; set; }
//}