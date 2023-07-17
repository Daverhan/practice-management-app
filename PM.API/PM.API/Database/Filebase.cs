using Newtonsoft.Json;
using PM.Library.Models;

namespace PM.API.Database
{
    public class Filebase
    {
        private string _root;
        private string _clientRoot;
        private string _projectRoot;
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
            _projectRoot = $"{_root}\\Projects";
        }
        private int LastClientId => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;

        public List<Client> Clients
        {
            get
            {
                var root = new DirectoryInfo(_clientRoot);
                var _clients = new List<Client>();
                foreach (var clientFile in root.GetFiles())
                {
                    var client = JsonConvert.DeserializeObject<Client>(File.ReadAllText(clientFile.FullName));
                    if (client != null)
                    {
                        _clients.Add(client);
                    }
                }
                return _clients;
            }
        }

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

        public bool DeleteClient(string id)
        {
            var path = $"{_clientRoot}\\{id}.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return true;
        }

        private int LastProjectId => Projects.Any() ? Projects.Select(p => p.Id).Max() : 0;

        public List<Project> Projects
        {
            get
            {
                var root = new DirectoryInfo(_projectRoot);
                var _projects = new List<Project>();
                foreach (var projectFile in root.GetFiles())
                {
                    var project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(projectFile.FullName));
                    if(project != null)
                    {
                        _projects.Add(project);
                    }
                }
                return _projects;
            }
        }

        public Project AddOrUpdate(Project project)
        {
            if (project.Id <= 0)
            {
                project.Id = LastProjectId + 1;
            }

            var path = $"{_projectRoot}\\{project.Id}.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(project));

            return project;
        }

        public bool DeleteProject(string id)
        {
            var path = $"{_projectRoot}\\{id}.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return true;
        }
    }
}
