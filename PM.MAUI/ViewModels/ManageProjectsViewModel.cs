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
        public ProjectViewModel SelectedProject { get; set; }
        public string BillsMessage { get; set; }
        public ObservableCollection<Bill> AssociatedBills { get; set; }
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

        public void UpdateSelectedDetails()
        {
            if(SelectedProject == null)
            {
                return;
            }

            BillsMessage = "Bills:";
            AssociatedBills = new ObservableCollection<Bill>(ProjectService.Current.GetProject(SelectedProject.Model.Id).Bills);

            NotifyPropertyChanged(nameof(BillsMessage));
            NotifyPropertyChanged(nameof(AssociatedBills));
        }

        public void RefreshView()
        {
            BillsMessage = null;
            AssociatedBills = null;
            SelectedProject = null;

            NotifyPropertyChanged(nameof(Projects));
            NotifyPropertyChanged(nameof(BillsMessage));
            NotifyPropertyChanged(nameof(AssociatedBills));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
