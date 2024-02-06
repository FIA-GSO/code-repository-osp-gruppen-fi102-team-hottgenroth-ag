using Logisitcs.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class TransportboxController : ControllerBase
   {
      protected ITransportboxBLL BLL { get; }

      public TransportboxController(ITransportboxBLL bll)
      {
         BLL = bll;
      }

      [HttpGet]
      public string Get()
      {
         
         return "";
      }
   }
}
