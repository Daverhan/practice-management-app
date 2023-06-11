using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ManageEmployeesViewModel : INotifyPropertyChanged
    {
        public Employee SelectedEmployee { get; set; }
        public string Query { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Employee>(EmployeeService.Current.Employees);
                }
                return new ObservableCollection<Employee>(EmployeeService.Current.Search(Query));
            }
        }

        public void Search()
        {
            RefreshView();
        }

        public void EditEmployeeClick(Shell s)
        {
            var idParam = SelectedEmployee?.Id ?? 0;

            if(idParam == 0)
            {
                return;
            }

            s.GoToAsync($"//EmployeeDetail?employeeId={idParam}");
        }

        public void Delete()
        {
            if (SelectedEmployee == null)
            {
                return;
            }

            EmployeeService.Current.DeleteEmployee(SelectedEmployee.Id);
            RefreshView();
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
