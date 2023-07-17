using PM.API.Database;
using PM.Library.DTO;
using PM.Library.Models;

namespace PM.API.EC
{
    public class ProjectEC
    {
        public ProjectDTO? Delete(int id)
        {
            Filebase.Current.DeleteProject(id.ToString());
            return Get(id);
        }

        public ProjectDTO AddOrUpdate(ProjectDTO dto)
        {
            return new ProjectDTO(Filebase.Current.AddOrUpdate(new Project(dto)));
        }

        public IEnumerable<ProjectDTO> Search(string query)
        {
            return Filebase.Current.Projects.Where(p => p.LongName.ToUpper().Contains(query.ToUpper())).Take(1000).Select(p => new ProjectDTO(p));
        }

        public ProjectDTO? Get(int id)
        {
            return new ProjectDTO(Filebase.Current.Projects.FirstOrDefault(p => p.Id == id) ?? new Project());
        }
    }
}
