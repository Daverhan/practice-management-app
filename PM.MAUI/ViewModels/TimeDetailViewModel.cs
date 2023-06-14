using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    public class TimeDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Hours { get; set; }
        public string Narrative { get; set; }
        public int Id { get; set; }
        public Project SelectedProject { get; set; }
        public Employee SelectedEmployee { get; set; }
        public string QueryProject { get; set; }
        public string QueryEmployee { get; set; }

        public TimeDetailViewModel(int id = 0)
        {
            if(id > 0)
            {
                LoadById(id);
            }
        }

        public void LoadById(int id)
        {
            if(id == 0)
            {
                return;
            }

            var time = TimeService.Current.GetTime(id);
            if(time != null)
            {
                Hours = time.Hours;
                Narrative = time.Narrative;
                SelectedProject = time.Project;
                SelectedEmployee = time.Employee;
                Id = time.Id;
            }

            NotifyPropertyChanged(nameof(Hours));
            NotifyPropertyChanged(nameof(Narrative));
            NotifyPropertyChanged(nameof(SelectedProject));
            NotifyPropertyChanged(nameof(SelectedEmployee));
        }

        public void AddTime()
        {
            if(Id <= 0)
            {
                TimeService.Current.AddTime(new Time
                {
                    Hours = Hours,
                    Narrative = Narrative,
                    Project = SelectedProject,
                    Employee = SelectedEmployee,
                    Date = DateTime.Now
                });
            }
            else
            {
                var timeToUpdate = TimeService.Current.GetTime(Id);
                timeToUpdate.Hours = Hours;
                timeToUpdate.Narrative = Narrative;
                timeToUpdate.Project = SelectedProject;
                timeToUpdate.Employee = SelectedEmployee;
            }
            Shell.Current.GoToAsync("//ManageTimes");
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

        public void SearchProject()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        public void SearchEmployee()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
