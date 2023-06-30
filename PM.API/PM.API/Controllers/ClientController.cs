using Microsoft.AspNetCore.Mvc;
using PM.API.Database;
using PM.API.EC;
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

        [HttpGet("/{id}")]
        public Client GetId(int id)
        {
            return FakeDatabase.Clients.FirstOrDefault(c => c.Id == id) ?? new Client();
        }

        [HttpDelete("/Delete/{id}")]
        public Client? Delete(int id)
        {
            return new ClientEC().Delete(id);
        }

        [HttpPost]
        public Client AddOrUpdate([FromBody]Client client)
        {
            return new ClientEC().AddOrUpdate(client);
        }
    }
}
