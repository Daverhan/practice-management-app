using PM.Library.Models;
using PM.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ClientDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public string Notes { get; set; }
        public string ClientStatusString { get; set; }
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public ClientDetailViewModel(int id = 0)
        {
            if(id > 0)
            {
                LoadById(id);
            }
            else
            {
                ClientStatusString = "A";
            }
        }

        public void LoadById(int id)
        {
            if (id == 0)
            {
                return;
            }

            var client = ClientService.Current.GetClient(id);
            if (client != null)
            {
                Name = client.Name;
                Notes = client.Notes;
                ClientStatusString = StatusToString(client.IsActive); 
                Id = client.Id;
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Notes));
            NotifyPropertyChanged(nameof(ClientStatusString));
        }

        public void AddClient()
        {
            if(Id <= 0)
            {
                ClientService.Current.AddClient(new Client
                {
                    Name = Name,
                    Notes = Notes,
                    IsActive = StringToStatus(ClientStatusString),
                    OpenDate = DateTime.Now,
                });
                Shell.Current.GoToAsync("//ManageClients");
            } 
            else
            {
                bool closeableClient = true;
                var clientToUpdate = ClientService.Current.GetClient(Id);
                clientToUpdate.Name = Name;
                clientToUpdate.Notes = Notes;

                if(ClientStatusString == "I")
                {
                    foreach (var project in ProjectService.Current.Projects)
                    {
                        if (project.Client?.Id == Id && project.IsActive == true)
                        {
                            closeableClient = false;
                            break;
                        }
                    }
                }

                if(closeableClient)
                {
                    if(!StringToStatus(ClientStatusString))
                    {
                        clientToUpdate.ClosedDate = DateTime.Now;
                    }

                    clientToUpdate.IsActive = StringToStatus(ClientStatusString);
                    Shell.Current.GoToAsync("//ManageClients");
                }
                else
                {
                    ErrorMessage = "Error: You are unable to close a client who is under an active project!";
                    NotifyPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        private bool StringToStatus(string s)
        {
            if (s == "A")
                return true;
            else
                return false;
        }

        private string StatusToString(bool status)
        {
            if (status)
                return "A";
            else
                return "I";
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
