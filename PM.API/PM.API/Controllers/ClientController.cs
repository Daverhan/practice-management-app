using Microsoft.AspNetCore.Mvc;
using PM.API.Database;
using PM.Library.Models;

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
        public IEnumerable<Client> Get()
        {
            return FakeDatabase.Clients;
        }
    }
}
