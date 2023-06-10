namespace PM.Library.Models
{
    public class Time
    {
        public DateTime Date { get; set; }
        public string? Narrative { get; set; }
        public int Hours { get; set; }
        public Project? Project { get; set; }
        public Employee? Employee { get; set; }
    }
}
