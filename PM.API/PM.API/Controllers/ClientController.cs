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

        [HttpGet("GetClients")]
        public IEnumerable<Client> Get()
        {
            return FakeDatabase.Clients;
        }

        [HttpGet("GetClients/{id}")]
        public Client GetId(int id)
        {
            return FakeDatabase.Clients.FirstOrDefault(c => c.Id == id) ?? new Client();
        }
    }
}
