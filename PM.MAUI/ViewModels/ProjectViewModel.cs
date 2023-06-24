using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PM.MAUI.ViewModels
{
    class ProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Client SelectedClient { get; set; }
        public string Query { get; set; }
        public Project Model { get; set; }
        public string ProjectStatusString { get; set; }

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
            Shell.Current.GoToAsync($"//ProjectDetail?projectId={id}");
        }

        public ICommand DeleteCommand { get; private set; }
        public void ExecuteDelete(int id)
        {
            ProjectService.Current.DeleteProject(id);
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command((p) => ExecuteDelete((p as ProjectViewModel).Model.Id));
            EditCommand = new Command((p) => ExecuteEdit((p as ProjectViewModel).Model.Id));
        }

        public ProjectViewModel(Project project)
        {
            Model = project;
            SetupCommands();
        }

        public ProjectViewModel(int projectId)
        {
            if(projectId == 0)
            {
                Model = new Project { IsActive = true };
            }
            else
            {
                Model = ProjectService.Current.GetProject(projectId);
                SelectedClient = Model.Client;
            }

            ProjectStatusString = StatusToString(Model.IsActive);
            NotifyPropertyChanged(nameof(SelectedClient));
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

        public void AddOrUpdate()
        {
            Model.IsActive = StringToStatus(ProjectStatusString);

            if (Model.IsActive)
            {
                Model.ClosedDate = new DateTime();
            }
            else
            {
                Model.ClosedDate = DateTime.Now;
            }

            Model.Client = SelectedClient;
            ProjectService.Current.AddOrUpdate(Model);
        }

        public ObservableCollection<Client> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Client>(ClientService.Current.Clients.ToList());
                }
                return new ObservableCollection<Client>(ClientService.Current.Search(Query).ToList());
            }
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Clients));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
