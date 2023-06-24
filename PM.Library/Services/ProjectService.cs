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
            projects = new List<Project>();
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

        public void AddOrUpdate(Project project)
        {
            if(project.Id == 0)
            {
                project.Id = LastId + 1;
                project.OpenDate = DateTime.Now;
                projects.Add(project);
            }
        }

        private int LastId
        {
            get
            {
                return Projects.Any() ? Projects.Select(p => p.Id).Max() : 0;
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
