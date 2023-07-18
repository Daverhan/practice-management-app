using Newtonsoft.Json;
using PM.Library.DTO;
using PM.Library.Models;
using PM.Library.Utilities;

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

        private List<EmployeeDTO> employees;

        private EmployeeService()
        {
            var response = new WebRequestHandler().Get("/Employee").Result;
            employees = JsonConvert.DeserializeObject<List<EmployeeDTO>>(response) ?? new List<EmployeeDTO>();
        }

        public List<EmployeeDTO> Employees
        {
            get { return employees; }
        }

        public List<EmployeeDTO> Search(string query)
        {
            return Employees.Where(e => e.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public EmployeeDTO? GetEmployee(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddOrUpdate(EmployeeDTO employee)
        {
            var response = new WebRequestHandler().Post("/Employee", employee).Result;
            var myUpdatedEmployee = JsonConvert.DeserializeObject<EmployeeDTO>(response);
            if (myUpdatedEmployee != null)
            {
                var existingEmployee = employees.FirstOrDefault(c => c.Id == myUpdatedEmployee.Id);
                if (existingEmployee == null)
                {
                    employees.Add(myUpdatedEmployee);
                }
                else
                {
                    var index = employees.IndexOf(existingEmployee);
                    employees.RemoveAt(index);
                    employees.Insert(index, myUpdatedEmployee);
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            var response = new WebRequestHandler().Delete($"/Employee/Delete/{id}").Result;

            var employeeToRemove = GetEmployee(id);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
            }
        }
    }
}
