namespace PM.Library.Models
{
    public class Client
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Boolean IsActive { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }

        public override string ToString()
        {
            return Id + ") " + Name;
        }
    }
}