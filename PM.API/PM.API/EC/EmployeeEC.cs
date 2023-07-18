using PM.API.Database;
using PM.Library.DTO;
using PM.Library.Models;

namespace PM.API.EC
{
    public class EmployeeEC
    {
        public EmployeeDTO? Delete(int id)
        {
            Filebase.Current.DeleteEmployee(id.ToString());
            return Get(id);
        }

        public EmployeeDTO AddOrUpdate(EmployeeDTO dto)
        {
            return new EmployeeDTO(Filebase.Current.AddOrUpdate(new Employee(dto)));
        }

        public IEnumerable<EmployeeDTO> Search(string query)
        {
            return Filebase.Current.Employees.Where(e => e.Name.ToUpper().Contains(query.ToUpper())).Take(1000).Select(e => new EmployeeDTO(e));
        }

        public EmployeeDTO? Get(int id)
        {
            return new EmployeeDTO(Filebase.Current.Employees.FirstOrDefault(e => e.Id == id) ?? new Employee());
        }
    }
}
