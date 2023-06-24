using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ManageEmployeesViewModel : INotifyPropertyChanged
    {
        public string Query { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<EmployeeViewModel> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<EmployeeViewModel>(EmployeeService.Current.Employees.Select(e => new EmployeeViewModel(e)).ToList());
                }
                return new ObservableCollection<EmployeeViewModel>(EmployeeService.Current.Search(Query).Select(e => new EmployeeViewModel(e)).ToList());
            }
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
