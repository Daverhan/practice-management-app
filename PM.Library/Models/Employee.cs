namespace PM.Library.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int Id { get; set; }

        public Employee()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Id + ") " + Name;
        }
    }
}
