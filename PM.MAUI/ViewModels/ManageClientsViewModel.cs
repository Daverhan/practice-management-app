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

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshView()
        {
            NotifyPropertyChanged("Clients");
        }
    }
}
