using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectBll projectBll;

        public ProjectController(IProjectBll projectBll)
        {
            this.projectBll = projectBll;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await projectBll.GetAllProjects();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await projectBll.GetProject(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IProjectData data)
        {
            var result = await projectBll.AddProject(data);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IProjectData data)
        {
            bool result = await projectBll.UpdateProject(data);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result = await projectBll.DeleteProject(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}