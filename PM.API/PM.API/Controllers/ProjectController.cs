using Microsoft.AspNetCore.Mvc;
using PM.API.EC;
using PM.Library.DTO;
using PM.Library.Utilities;

namespace PM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ProjectDTO> Get()
        {
            return new ProjectEC().Search(string.Empty);
        }

        [HttpGet("{id}")]
        public ProjectDTO? GetId(int id)
        {
            return new ProjectEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public ProjectDTO? Delete(int id)
        {
            return new ProjectEC().Delete(id);
        }

        [HttpPost]
        public ProjectDTO AddOrUpdate([FromBody]ProjectDTO project)
        {
            return new ProjectEC().AddOrUpdate(project);
        }

        [HttpPost("Search")]
        public IEnumerable<ProjectDTO> Search([FromBody]QueryMessage query)
        {
            return new ProjectEC().Search(query.Query);
        }
    }
}
