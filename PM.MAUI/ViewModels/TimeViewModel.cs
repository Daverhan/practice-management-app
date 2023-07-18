using PM.Library.DTO;
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
        public ProjectDTO SelectedProject { get; set; }
        public EmployeeDTO SelectedEmployee { get; set; }
        public string QueryProject { get; set; }
        public string QueryEmployee { get; set; }
        public TimeDTO Model { get; set; }
        public string DisplaySelectedProject { get; set; }
        public string DisplaySelectedEmployee { get; set; }
        public string ErrorMessage { get; set; }

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

        public TimeViewModel(TimeDTO time)
        {
            Model = time;
            SetupCommands();
        }

        public TimeViewModel(int timeId)
        {
            if(timeId == 0)
            {
                DisplaySelectedProject = "Select Project";
                DisplaySelectedEmployee = "Select Employee";
                NotifyPropertyChanged(nameof(DisplaySelectedProject));
                NotifyPropertyChanged(nameof(DisplaySelectedEmployee));
                Model = new TimeDTO();
            }
            else
            {
                Model = TimeService.Current.GetTime(timeId);
                DisplaySelectedProject = "Current Project: " + Model.Project.ToString();
                DisplaySelectedEmployee = "Current Employee: " + Model.Employee.ToString();
                NotifyPropertyChanged(nameof(DisplaySelectedProject));
                NotifyPropertyChanged(nameof(DisplaySelectedEmployee));
                SelectedProject = Model.Project;
                SelectedEmployee = Model.Employee;
            }

            NotifyPropertyChanged(nameof(SelectedProject));
            NotifyPropertyChanged(nameof(SelectedEmployee));
            SetupCommands();
        }

        public bool AddOrUpdate()
        {
            if(Model.Id == 0 && SelectedProject == null || SelectedEmployee == null)
            {
                ErrorMessage = "Error: You must select a project and an employee to create this time entry!";
                NotifyPropertyChanged(nameof(ErrorMessage));
                return false;
            }

            Model.Employee = SelectedEmployee;
            Model.Project = SelectedProject;

            if(SelectedProject != null && SelectedEmployee != null)
            {
                Model.Project = SelectedProject;
                Model.Employee = SelectedEmployee;
            }
            TimeService.Current.AddOrUpdate(Model);
            return true;
        }

        public ObservableCollection<ProjectDTO> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(QueryProject))
                {
                    return new ObservableCollection<ProjectDTO>(ProjectService.Current.Projects);
                }
                return new ObservableCollection<ProjectDTO>(ProjectService.Current.Search(QueryProject));
            }
        }

        public ObservableCollection<EmployeeDTO> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(QueryEmployee))
                {
                    return new ObservableCollection<EmployeeDTO>(EmployeeService.Current.Employees);
                }
                return new ObservableCollection<EmployeeDTO>(EmployeeService.Current.Search(QueryEmployee));
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
