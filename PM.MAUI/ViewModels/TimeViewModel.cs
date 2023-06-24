using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PM.MAUI.ViewModels
{
    public class TimeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Project SelectedProject { get; set; }
        public Employee SelectedEmployee { get; set; }
        public string QueryProject { get; set; }
        public string QueryEmployee { get; set; }
        public Time Model { get; set; }

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
            Shell.Current.GoToAsync($"//TimeDetail?timeId={id}");
        }

        public ICommand DeleteCommand { get; private set; }
        public void ExecuteDelete(int id)
        {
            TimeService.Current.DeleteTime(id);
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command((t) => ExecuteDelete((t as TimeViewModel).Model.Id));
            EditCommand = new Command((t) => ExecuteEdit((t as TimeViewModel).Model.Id));
        }

        public TimeViewModel(Time time)
        {
            Model = time;
            SetupCommands();
        }

        public TimeViewModel(int timeId)
        {
            if(timeId == 0)
            {
                Model = new Time();
            }
            else
            {
                Model = TimeService.Current.GetTime(timeId);
                SelectedProject = Model.Project;
                SelectedEmployee = Model.Employee;
            }

            NotifyPropertyChanged(nameof(SelectedProject));
            NotifyPropertyChanged(nameof(SelectedEmployee));
            SetupCommands();
        }

        public void AddOrUpdate()
        {
            Model.Employee = SelectedEmployee;
            Model.Project = SelectedProject;
            TimeService.Current.AddOrUpdate(Model);
        }

        public ObservableCollection<Project> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(QueryProject))
                {
                    return new ObservableCollection<Project>(ProjectService.Current.Projects);
                }
                return new ObservableCollection<Project>(ProjectService.Current.Search(QueryProject));
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(QueryEmployee))
                {
                    return new ObservableCollection<Employee>(EmployeeService.Current.Employees);
                }
                return new ObservableCollection<Employee>(EmployeeService.Current.Search(QueryEmployee));
            }
        }

        public void RefreshProjectsList()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        public void RefreshEmployeesList()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
