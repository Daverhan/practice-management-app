using PM.Library.Models;
using PM.Library.Services;
using System.Windows.Input;

namespace PM.MAUI.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Model { get; set; }

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ICommand EditCommand { get; private set; }
        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//EmployeeDetail?employeeId={id}");
        }

        public ICommand DeleteCommand { get; private set; }
        public void ExecuteDelete(int id)
        {
            EmployeeService.Current.DeleteEmployee(id);
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command((e) => ExecuteDelete((e as EmployeeViewModel).Model.Id));
            EditCommand = new Command((e) => ExecuteEdit((e as EmployeeViewModel).Model.Id));
        }

        public EmployeeViewModel(Employee employee)
        {
            Model = employee;
            SetupCommands();
        }

        public EmployeeViewModel(int employeeId)
        {
            if(employeeId == 0)
            {
                Model = new Employee();
            }
            else
            {
                Model = EmployeeService.Current.GetEmployee(employeeId);
            }

            SetupCommands();
        }

        public void AddOrUpdate()
        {
            EmployeeService.Current.AddOrUpdate(Model);
        }
    }
}
