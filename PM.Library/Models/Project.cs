namespace PM.Library.Models
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Boolean IsActive { get; set; }
        public String? ShortName { get; set; }
        public String? LongName { get; set; }
        public Client? Client { get; set; }

        public override string ToString()
        {
            return "\nLong Name: " + LongName + "\nShort Name: " + ShortName + "\nID: " + Id + "\nActive Account: " + IsActive + "\nAccount Opened: " + OpenDate.ToString() + "\nAccount Closed: " + ClosedDate.ToString() + "\nClient: " + Client.Name;
        }
    }
}