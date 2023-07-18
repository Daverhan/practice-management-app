using PM.Library.Models;

namespace PM.Library.DTO
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int Id { get; set; }

        public EmployeeDTO()
        {
            Name = string.Empty;
        }

        public EmployeeDTO(Employee employee)
        {
            this.Id = employee.Id;
            this.Name = employee.Name;
            this.Rate = employee.Rate;
        }

        public override string ToString()
        {
            return Id + ") " + Name;
        }
    }
}
