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

        //private List<Client> clients;

        private ClientService()
        {
            //var response = new WebRequestHandler().Get("/Client").Result;
            //clients = JsonConvert.DeserializeObject<List<Client>>(response) ?? new List<Client>();
        }

        public List<Client> Clients
        {
            get
            {
                var response = new WebRequestHandler().Get("/Client").Result;
                return JsonConvert.DeserializeObject<List<Client>>(response) ?? new List<Client>();
            }
        }

        public List<Client> Search(string query)
        {
            return Clients.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public Client? GetClient(int id)
        {
            return Clients.FirstOrDefault(c => c.Id == id);
        }

        public void AddOrUpdate(Client client)
        {
            var response = new WebRequestHandler().Post("/Client", client).Result;
        }

        public void DeleteClient(int id)
        {
            var response = new WebRequestHandler().Delete($"/Delete/{id}").Result;
        }
    }
}
