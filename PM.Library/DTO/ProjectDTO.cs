using PM.Library.Models;

namespace PM.Library.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Boolean IsActive { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public ClientDTO? Client { get; set; }
        public List<Bill>? Bills { get; set; }

        public ProjectDTO()
        {
            LongName = string.Empty;
            ShortName = string.Empty;
        }

        public ProjectDTO(Project project)
        {
            this.Id = project.Id;
            this.OpenDate = project.OpenDate;
            this.ClosedDate = project.ClosedDate;
            this.IsActive = project.IsActive;
            this.ShortName = project.ShortName;
            this.LongName = project.LongName;
            this.Client = project.Client;
            this.Bills = project.Bills;
        }

        public override string ToString()
        {
            return Id + ") " + LongName;
        }
    }
}
