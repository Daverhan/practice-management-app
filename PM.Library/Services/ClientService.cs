using Newtonsoft.Json;
using PM.Library.DTO;
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

        private List<ClientDTO> clients;

        private ClientService()
        {
            var response = new WebRequestHandler().Get("/Client").Result;
            clients = JsonConvert.DeserializeObject<List<ClientDTO>>(response) ?? new List<ClientDTO>();
        }

        public List<ClientDTO> Clients
        {
            get
            {
                return clients;
            }
        }

        public List<ClientDTO> Search(string query)
        {
            var response = new WebRequestHandler().Post("/Search", new QueryMessage(query)).Result;
            return JsonConvert.DeserializeObject<List<ClientDTO>>(response) ?? new List<ClientDTO>();
        }

        public ClientDTO? GetClient(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        public void AddOrUpdate(ClientDTO client)
        {
            var response = new WebRequestHandler().Post("/Client", client).Result;

            var myUpdatedClient = JsonConvert.DeserializeObject<ClientDTO>(response);
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

            var clientToDelete = GetClient(id);
            if(clientToDelete != null)
            {
                clients.Remove(clientToDelete);
            }
        }
    }
}
