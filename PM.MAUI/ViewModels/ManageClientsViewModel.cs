using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ManageClientsViewModel : INotifyPropertyChanged
    {
        public Client SelectedClient { get; set; }
        public string Query { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string DateOpened { get; set; }
        public string DateClosed { get; set; }
        public string Notes { get; set; }
        public string ProjectsMessage { get; set; }
        public ObservableCollection<Project> AssociatedProjects { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Client> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Client>(ClientService.Current.Clients);
                }
                return new ObservableCollection<Client>(ClientService.Current.Search(Query));
            }
        }

        public void Search()
        {
            RefreshView();
        }

        public void EditClientClick(Shell s)
        {
            var idParam = SelectedClient?.Id ?? 0;

            if(idParam == 0)
            {
                return;
            }

            s.GoToAsync($"//ClientDetail?clientId={idParam}");
        }

        public void Delete()
        {
            if (SelectedClient == null)
            {
                return;
            }

            ClientService.Current.DeleteClient(SelectedClient.Id);
            RefreshView();
        }

        public void UpdateSelectedDetails()
        {
            if(SelectedClient == null)
            {
                return;
            }

            Name = "Name: " + SelectedClient?.Name;
            Status = "Status: " + StatusToString(SelectedClient.IsActive);
            DateOpened = "Date Opened: " + SelectedClient?.OpenDate.ToString();
            DateClosed = SelectedClient?.ClosedDate.Year == 0001 ? "Date Closed: N/A" : "Date Closed: " + SelectedClient?.ClosedDate.ToString();
            Notes = "Notes: " + SelectedClient?.Notes;
            ProjectsMessage = "Projects: ";
            AssociatedProjects = new ObservableCollection<Project>(ProjectService.Current.Projects.Where(p => p.Client == SelectedClient).ToList());

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
            if (status)
                return "Active";
            else
                return "Inactive";
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
