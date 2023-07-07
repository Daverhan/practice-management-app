using PM.API.Database;
using PM.Library.Models;

namespace PM.API.EC
{
    public class ClientEC
    {
        public Client? Delete(int id)
        {
            var clientToDelete = FakeDatabase.Clients.FirstOrDefault(c => c.Id == id);

            if (clientToDelete != null)
            {
                FakeDatabase.Clients.Remove(clientToDelete);
            }

            return clientToDelete;
        }

        public Client AddOrUpdate(Client client)
        {
            if (client.Id > 0)
            {
                var clientToUpdate = FakeDatabase.Clients.FirstOrDefault(c => c.Id == client.Id);

                if (clientToUpdate != null)
                {
                    FakeDatabase.Clients.Remove(clientToUpdate);
                }

                FakeDatabase.Clients.Add(client);
            }
            else
            {
                client.Id = FakeDatabase.LastClientId + 1;
                FakeDatabase.Clients.Add(client);
            }

            return client;
        }

        public IEnumerable<Client> Search(string query)
        {
            return FakeDatabase.Clients.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).Take(1000);
        }
    }
}
