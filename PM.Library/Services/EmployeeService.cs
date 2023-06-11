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
        private int IdCounter = 1;

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

        public void AddEmployee(Employee? employee)
        {
            if(employee != null)
            {
                employee.Id = IdCounter++;
                employees.Add(employee);
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
