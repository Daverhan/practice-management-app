using PM.Library.Models;

namespace PM.Library.Services
{
    public class ClientService
    {
        private static ClientService? instance;
        private static object _lock = new object();
        public static ClientService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ClientService();
                    }
                }

                return instance;
            }
        }

        private List<Client> clients;

        private ClientService()
        {
            clients = new List<Client>();
        }

        public List<Client> Clients
        {
            get { return clients; }
        }

        public List<Client> Search(string query)
        {
            return Clients.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public Client? GetClient(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        public void AddOrUpdate(Client client)
        {
            if (client.Id == 0)
            {
                client.Id = LastId + 1;
                client.OpenDate = DateTime.Now;
                clients.Add(client);
            }
        }

        private int LastId
        {
            get
            {
                return Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
            }
        }

        public void DeleteClient(int id)
        {
            var clientToRemove = GetClient(id);

            if (clientToRemove != null)
            {
                clients.Remove(clientToRemove);
            }
        }
    }
}