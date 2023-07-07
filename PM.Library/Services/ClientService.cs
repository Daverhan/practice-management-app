using Newtonsoft.Json;
using PM.Library.Models;
using PM.Library.Utilities;

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
            var response = new WebRequestHandler().Get("/Client").Result;
            clients = JsonConvert.DeserializeObject<List<Client>>(response) ?? new List<Client>();
        }

        public List<Client> Clients
        {
            get
            {
                return clients;
            }
        }

        public List<Client> Search(string query)
        {
            return clients.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public Client? GetClient(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        public void AddOrUpdate(Client client)
        {
            var response = new WebRequestHandler().Post("/Client", client).Result;

            var myUpdatedClient = JsonConvert.DeserializeObject<Client>(response);
            if(myUpdatedClient != null)
            {
                var existingClient = clients.FirstOrDefault(c => c.Id == myUpdatedClient.Id);
                if(existingClient == null)
                {
                    clients.Add(myUpdatedClient);
                }
                else
                {
                    var index = clients.IndexOf(existingClient);
                    clients.RemoveAt(index);
                    clients.Insert(index, myUpdatedClient);
                }
            }
        }

        public void DeleteClient(int id)
        {
            var response = new WebRequestHandler().Delete($"/Delete/{id}").Result;
        }
    }
}
