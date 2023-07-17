using Newtonsoft.Json;
using PM.Library.DTO;
using PM.Library.Utilities;

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

        private List<ProjectDTO> projects;

        private ProjectService()
        {
            var response = new WebRequestHandler().Get("/Project").Result;
            projects = JsonConvert.DeserializeObject<List<ProjectDTO>>(response) ?? new List<ProjectDTO>();
        }

        public List<ProjectDTO> Projects
        {
            get { return projects; }
        }

        public List<ProjectDTO> Search(string query)
        {
            return Projects.Where(p => p.LongName.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public ProjectDTO? GetProject(int id)
        {
            return projects.FirstOrDefault(p => p.Id == id);
        }

        public void AddOrUpdate(ProjectDTO project)
        {
            var response = new WebRequestHandler().Post("/Project", project).Result;
            var myUpdatedProject = JsonConvert.DeserializeObject<ProjectDTO>(response);
            if(myUpdatedProject != null)
            {
                var existingProject = projects.FirstOrDefault(p => p.Id == myUpdatedProject.Id);
                if(existingProject == null)
                {
                    projects.Add(myUpdatedProject);
                }
                else
                {
                    var index = projects.IndexOf(existingProject);
                    projects.RemoveAt(index);
                    projects.Insert(index, myUpdatedProject);
                }
            }
        }

        public void DeleteProject(int id)
        {
            var response = new WebRequestHandler().Delete($"/Project/Delete/{id}").Result;

            var projectToDelete = GetProject(id);
            if(projectToDelete != null)
            {
                projects.Remove(projectToDelete);
            }
        }
    }
}
