using Microsoft.AspNetCore.Mvc;
using PM.API.EC;
using PM.Library.DTO;
using PM.Library.Utilities;

namespace PM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ClientDTO> Get() 
        {
            return new ClientEC().Search(string.Empty);
        }

        [HttpGet("{id}")]
        public ClientDTO? GetId(int id)
        {
            return new ClientEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public ClientDTO? Delete(int id)
        {
            return new ClientEC().Delete(id);
        }

        [HttpPost]
        public ClientDTO AddOrUpdate([FromBody]ClientDTO client)
        {
            return new ClientEC().AddOrUpdate(client);
        }
        
        [HttpPost("Search")]
        public IEnumerable<ClientDTO> Search([FromBody]QueryMessage query)
        {
            return new ClientEC().Search(query.Query);
        }
    }
}
