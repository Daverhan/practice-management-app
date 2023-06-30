using PM.API.EC;
using PM.Library.Models;

namespace PM.API.Database
{
    public static class FakeDatabase
    {
        public static List<Client> Clients = new List<Client>
        {
            new Client {Id = 1, Name = "Client 1", IsActive = true },
            new Client {Id = 2, Name = "Client 2", IsActive = true },
            new Client {Id = 3, Name = "Client 3", IsActive = true },
            new Client {Id = 4, Name = "Client 4", IsActive = true },
            new Client {Id = 5, Name = "Client 5", IsActive = true }
        };

        public static int LastClientId => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
    }
}
