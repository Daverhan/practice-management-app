using PM.Library.Models;

namespace PM.Library.Services
{
    public class ProjectService
    {
        private static ProjectService? instance;
        private static object _lock = new object();
        public static ProjectService Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new ProjectService();
                    }
                }

                return instance;
            }
        }

        private List<Project> projects;

        private ProjectService()
        {
            projects = new List<Project>
            {
                new Project{Id = 1, LongName = "My First Project" },
                new Project{Id = 2, LongName = "My Second Project" },
                new Project{Id = 3, LongName = "My Third Project" }
            };
        }

        public List<Project> Projects
        {
            get { return projects; }
        }

        public List<Project> Search(string query)
        {
            return Projects.Where(p => p.LongName.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public Project? GetProject(int id)
        {
            return projects.FirstOrDefault(p => p.Id == id);
        }

        public void AddProject(Project? project)
        {
            if(project != null)
            {
                projects.Add(project);
            }
        }

        public void DeleteProject(int id)
        {
            var projectToRemove = GetProject(id);

            if (projectToRemove != null)
            {
                projects.Remove(projectToRemove);
            }
        }
    }
}
