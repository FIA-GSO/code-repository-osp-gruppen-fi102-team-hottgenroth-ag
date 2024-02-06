using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class TransportboxController : ControllerBase
   {
      public TransportboxController()
      {
       
      }

      [HttpGet]
      public string Get()
      {
         return "";
      }
   }
}
