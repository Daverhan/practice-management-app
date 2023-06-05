namespace PM.Library.Models
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Boolean IsActive { get; set; }
        public string? ShortName { get; set; }
        public string? LongName { get; set; }
        public Client? Client { get; set; }

        public override string ToString()
        {
            return Id + ". " + LongName;
        }
    }
}