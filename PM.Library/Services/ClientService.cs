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
            clients = new List<Client>();
        }

        public Client? GetClient(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        public Client? GetClient(string name)
        {
            return clients.FirstOrDefault(c => c.Name == name);
        }

        public int GetSize()
        {
            return clients.Count;
        }

        public void AddClient(Client? client)
        {
            if(client != null)
            {
                clients.Add(client);
            }
        }

        public bool DeleteClient(int id)
        {
            var clientToRemove = GetClient(id);

            if(clientToRemove != null)
            {
                clients.Remove(clientToRemove);
                return true;
            }
            return false;
        }

        public void DisplayLongDetails()
        {
            clients.ForEach(Console.WriteLine);
        }

        public void DisplayShortDetails()
        {
            foreach (var client in clients)
            {
                Console.WriteLine(client.Id + ") " + client.Name);
            }
        }
    }
}
