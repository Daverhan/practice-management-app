using PM.Library.Models;
using PM.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PM.MAUI.ViewModels
{
    class ClientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Client Model { get; set; }
        public string ClientStatusString { get; set; }
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
            Shell.Current.GoToAsync($"//ClientDetail?clientId={id}");
        }

        public ICommand DeleteCommand { get; private set; }
        public void ExecuteDelete(int id)
        {
            ClientService.Current.DeleteClient(id);
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command((c) => ExecuteDelete((c as ClientViewModel).Model.Id));
            EditCommand = new Command((c) => ExecuteEdit((c as ClientViewModel).Model.Id));
        }

        public ClientViewModel(Client client)
        {
            Model = client;
            SetupCommands();
        }

        public ClientViewModel(int clientId)
        {
            if(clientId == 0)
            {
                Model = new Client {IsActive = true};
            }
            else
            {
                Model = ClientService.Current.GetClient(clientId);
            }

            ClientStatusString = StatusToString(Model.IsActive);
            SetupCommands();
        }

        private string StatusToString(bool status)
        {
            return status ? "A" : "I";
        }

        private bool StringToStatus(string status)
        {
            return status == "A" ? true : false;
        }

        public bool AddOrUpdate()
        {
            if(Model.Id > 0)
            {
                if (ClientStatusString == "I")
                {
                    foreach (var project in ProjectService.Current.Projects)
                    {
                        if (project.Client?.Id == Model.Id && project.IsActive)
                        {
                            ErrorMessage = "Error: You are unable to close a client who is under an active project!";
                            NotifyPropertyChanged(nameof(ErrorMessage));
                            return false;
                        }
                    }
                }
            }

            Model.IsActive = StringToStatus(ClientStatusString);

            if(Model.IsActive)
            {
                Model.ClosedDate = new DateTime();
            }
            else
            {
                Model.ClosedDate = DateTime.Now;
            }

            ClientService.Current.AddOrUpdate(Model);
            return true;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
