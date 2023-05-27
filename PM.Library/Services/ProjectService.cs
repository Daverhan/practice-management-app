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
                    if (instance == null) { }
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

        public Project? GetProject(int id)
        {
            return projects.FirstOrDefault(p => p.Id == id);
        }

        public int GetSize()
        {
            return projects.Count;
        }

        public void AddProject(Project? project)
        {
            if(project != null)
            {
                projects.Add(project);
            }
        }

        public bool DeleteProject(int id)
        {
            var projectToRemove = GetProject(id);

            if (projectToRemove != null)
            {
                projects.Remove(projectToRemove);
                return true;
            }
            return false;
        }

        public void DisplayLongDetails()
        {
            projects.ForEach(Console.WriteLine);
        }

        public void DisplayShortDetails()
        {
            foreach (var project in projects) {
                Console.WriteLine(project.Id + ") " + project.LongName);
            }
        }
    }
}
