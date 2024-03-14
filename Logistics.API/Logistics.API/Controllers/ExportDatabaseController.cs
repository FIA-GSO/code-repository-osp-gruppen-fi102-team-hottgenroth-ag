using Logisitcs.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
   [Authorize]
   [ApiController]
   [Route("[controller]")]
   public class ExportDatabaseController : ControllerBase
   {
      private readonly IExportDatabaseBll bll;
      public ExportDatabaseController(IExportDatabaseBll exportBll)
      {
         bll = exportBll;
      }

      [HttpGet, DisableRequestSizeLimit]
      public async Task<IActionResult> GetStreamCatalog()
      {
         var stream = await bll.GetStreamLogisticsDb();
         if (stream == null)
            return NotFound();

         return File(stream.ToArray(), "application/x-sqlite3");
      }
   }
}
