using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ManageProjectsViewModel : INotifyPropertyChanged
    {
        public string Query { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<ProjectViewModel>(ProjectService.Current.Projects.Select(p => new ProjectViewModel(p)).ToList());
                }
                return new ObservableCollection<ProjectViewModel>(ProjectService.Current.Search(Query).Select(p => new ProjectViewModel(p)).ToList());
            }
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
