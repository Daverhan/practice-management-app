using Newtonsoft.Json;
using PM.Library.Models;

namespace PM.API.Database
{
    public class Filebase
    {
        private string _root;
        private string _clientRoot;
        private static Filebase? _instance;
        private static object _lock = new object();

        public static Filebase Current
        {
            get
            {
                lock(_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Filebase();
                    }
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = "C:\\PM Database";
            _clientRoot = $"{_root}\\Clients";
        }
        private int LastClientId => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;

        public Client AddOrUpdate(Client client)
        {
            if(client.Id <= 0)
            {
                client.Id = LastClientId + 1;
            }

            var path = $"{_clientRoot}\\{client.Id}.json";

            if(File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(client));

            return client;
        }

        public List<Client> Clients
        {
            get
            {
                var root = new DirectoryInfo(_clientRoot);
                var _clients = new List<Client>();
                foreach (var clientFile in root.GetFiles())
                {
                    var client = JsonConvert.DeserializeObject<Client>(File.ReadAllText(clientFile.FullName));
                    if(client != null)
                    {
                        _clients.Add(client);
                    }
                }
                return _clients;
            }
        }

        public bool Delete(string id)
        {
            var path = $"{_clientRoot}\\{id}.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return true;
        }
    }
}
