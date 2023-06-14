using PM.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class EmployeeDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int Id { get; set; }

        public EmployeeDetailViewModel(int id = 0)
        {
            if (id > 0)
            {
                LoadById(id);
            }
        }

        public void LoadById(int id)
        {
            if (id == 0)
            {
                return;
            }

            var employee = EmployeeService.Current.GetEmployee(id);
            if (employee != null)
            {
                Name = employee.Name;
                Rate = employee.Rate;
                Id = employee.Id;
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Rate));
        }

        public void AddEmployee()
        {
            if (Id <= 0)
            {
                EmployeeService.Current.AddEmployee(new Library.Models.Employee
                {
                    Name = Name,
                    Rate = Rate,
                });
            }
            else
            {
                var employeeToUpdate = EmployeeService.Current.GetEmployee(Id);
                employeeToUpdate.Name = Name;
                employeeToUpdate.Rate = Rate;
            }
            Shell.Current.GoToAsync("//ManageEmployees");
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
