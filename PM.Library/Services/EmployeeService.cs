using PM.Library.Models;

namespace PM.Library.Services
{
    public class EmployeeService
    {
        private static EmployeeService? instance;
        private static object _lock = new object();
        public static EmployeeService Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeService();
                    }
                }

                return instance;
            }
        }

        private List<Employee> employees;

        private EmployeeService()
        {
            employees = new List<Employee>();
        }

        public List<Employee> Employees
        {
            get { return employees; }
        }

        public List<Employee> Search(string query)
        {
            return Employees.Where(e => e.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public Employee? GetEmployee(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddOrUpdate(Employee employee)
        {
            if(employee.Id == 0)
            {
                employee.Id = LastId + 1;
                employees.Add(employee);
            }
        }

        private int LastId
        {
            get
            {
                return Employees.Any() ? Employees.Select(e => e.Id).Max() : 0;
            }
        }

        public void DeleteEmployee(int id)
        {
            var employeeToRemove = GetEmployee(id);

            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
            }
        }
    }
}
