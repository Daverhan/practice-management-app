namespace PM.Library.Models
{
    public class Client
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Boolean IsActive { get; set; }
        public String? Name { get; set; }
        public String? Notes { get; set; }

        public override string ToString()
        {
            return "\nName: " + Name + "\nID: " + Id + "\nActive Account: " + IsActive + "\nAccount Opened: " + OpenDate.ToString() + "\nAccount Closed: " + ClosedDate.ToString() + "\nNotes:\n" + Notes;
        }
    }
}