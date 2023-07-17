using PM.Library.DTO;

namespace PM.Library.Models
{
    public class Time
    {
        public DateTime Date { get; set; }
        public string? Narrative { get; set; }
        public int Hours { get; set; }
        public int Id { get; set; }
        public ProjectDTO? Project { get; set; }
        public Employee? Employee { get; set; }

        public override string ToString()
        {
            return Id + ") " + Employee?.Name + " wrote a record for Project \"" + Project?.LongName + "\"";
        }
    }
}
