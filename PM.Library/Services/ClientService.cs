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
                lock(_lock)
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
            clients = new List<Client>
            {
                new Client{Id = 1, Name = "John Smith" },
                new Client{Id = 2, Name = "Bob Smith" },
                new Client{Id = 3, Name = "Sue Smith" }
            };
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

        public void AddClient(Client? client)
        {
            if (client != null)
            {
                clients.Add(client);
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
