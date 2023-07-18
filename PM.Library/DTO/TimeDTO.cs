using PM.Library.Models;

namespace PM.Library.DTO
{
    public class TimeDTO
    {
        public DateTime Date { get; set; }
        public string? Narrative { get; set; }
        public int Hours { get; set; }
        public int Id { get; set; }
        public ProjectDTO? Project { get; set; }
        public EmployeeDTO? Employee { get; set; }

        public TimeDTO()
        {

        }

        public TimeDTO(Time time)
        {
            this.Date = time.Date;
            this.Narrative = time.Narrative;
            this.Hours = time.Hours;
            this.Id = time.Id;
            this.Project = time.Project;
            this.Employee = time.Employee;
        }

        public override string ToString()
        {
            return Id + ") " + Employee?.Name + " wrote a record for Project \"" + Project?.LongName + "\"";
        }
    }
}
