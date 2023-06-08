using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ManageProjectsViewModel : INotifyPropertyChanged
    {
        public Project SelectedProject { get; set; }
        public string Query { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Project> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Project>(ProjectService.Current.Projects);
                }
                return new ObservableCollection<Project>(ProjectService.Current.Search(Query));
            }
        }

        public void Search()
        {
            RefreshView();
        }

        public void EditProjectClick(Shell s)
        {
            var idParam = SelectedProject?.Id ?? 0;

            if(idParam == 0)
            {
                return;
            }

            s.GoToAsync($"//ProjectDetail?projectId={idParam}");
        }

        public void Delete()
        {
            if (SelectedProject == null)
            {
                return;
            }

            ProjectService.Current.DeleteProject(SelectedProject.Id);
            RefreshView();
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
