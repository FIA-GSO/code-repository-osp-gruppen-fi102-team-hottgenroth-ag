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
      public string Get()
      {
         return "";
      }
   }
}
