using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class ProjectController : ControllerBase
   {
      public ProjectController()
      {
       
      }

      [HttpGet]
      public string Get()
      {
         return "";
      }
   }
}
