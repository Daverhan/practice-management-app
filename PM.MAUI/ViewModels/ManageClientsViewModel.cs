using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ManageClientsViewModel : INotifyPropertyChanged
    {
        public ClientViewModel SelectedClient { get; set; }
        public string Query { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string DateOpened { get; set; }
        public string DateClosed { get; set; }
        public string Notes { get; set; }
        public string ProjectsMessage { get; set; }
        public ObservableCollection<Project> AssociatedProjects { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ClientViewModel> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<ClientViewModel>(ClientService.Current.Clients.Select(c => new ClientViewModel(c)).ToList());
                }
                return new ObservableCollection<ClientViewModel>(ClientService.Current.Search(Query).Select(c => new ClientViewModel(c)).ToList());
            }
        }

        public void UpdateSelectedDetails()
        {
            if(SelectedClient == null)
            {
                return;
            }

            Name = "Name: " + SelectedClient.Model.Name;
            Status = "Status: " + StatusToString(SelectedClient.Model.IsActive);
            DateOpened = "Date Opened: " + SelectedClient.Model.OpenDate.ToShortDateString();
            DateClosed = SelectedClient.Model.ClosedDate.Year == 0001 ? "Date Closed: N/A" : "Date Closed: " + SelectedClient.Model.ClosedDate.ToShortDateString();
            Notes = "Notes: " + SelectedClient.Model.Notes;
            ProjectsMessage = "Projects: ";
            AssociatedProjects = new ObservableCollection<Project>(ProjectService.Current.Projects.Where(p => p.Client == SelectedClient.Model).ToList());

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Status));
            NotifyPropertyChanged(nameof(DateOpened));
            NotifyPropertyChanged(nameof(DateClosed));
            NotifyPropertyChanged(nameof(Notes));
            NotifyPropertyChanged(nameof(ProjectsMessage));
            NotifyPropertyChanged(nameof(AssociatedProjects));
        }

        public void RefreshView()
        {
            Name = Status = DateOpened = DateClosed = Notes = ProjectsMessage = null;
            AssociatedProjects = null;
            SelectedClient = null;

            NotifyPropertyChanged(nameof(Clients));
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Status));
            NotifyPropertyChanged(nameof(DateOpened));
            NotifyPropertyChanged(nameof(DateClosed));
            NotifyPropertyChanged(nameof(Notes));
            NotifyPropertyChanged(nameof(ProjectsMessage));
            NotifyPropertyChanged(nameof(SelectedClient));
            NotifyPropertyChanged(nameof(AssociatedProjects));
        }

        private string StatusToString(bool status)
        {
            return status ? "Active" : "Inactive";
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
