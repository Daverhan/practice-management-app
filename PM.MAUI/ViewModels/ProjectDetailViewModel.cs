using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ProjectDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string ProjectStatusString { get; set; }
        public int Id { get; set; }
        public Client SelectedClient { get; set; }
        public string Query { get; set; }

        public ProjectDetailViewModel(int id = 0)
        {
            if(id > 0)
            {
                LoadById(id);
            }
            else
            {
                ProjectStatusString = "A";
            }
        }

        public void LoadById(int id)
        {
            if (id == 0)
            {
                return;
            }

            var project = ProjectService.Current.GetProject(id);
            if (project != null)
            {
                LongName = project.LongName;
                ShortName = project.ShortName;
                ProjectStatusString = StatusToString(project.IsActive);
                SelectedClient = project.Client;
                Id = project.Id;
            }

            NotifyPropertyChanged(nameof(LongName));
            NotifyPropertyChanged(nameof(ShortName));
            NotifyPropertyChanged(nameof(ProjectStatusString));
            NotifyPropertyChanged(nameof(SelectedClient));
        }

        public void AddProject()
        {
            if(Id <= 0)
            {
                ProjectService.Current.AddProject(new Project
                {
                    LongName = LongName,
                    ShortName = ShortName,
                    IsActive = StringToStatus(ProjectStatusString),
                    Client = SelectedClient
                });
            }
            else
            {
                var projectToUpdate = ProjectService.Current.GetProject(Id);
                projectToUpdate.LongName = LongName;
                projectToUpdate.ShortName = ShortName;
                projectToUpdate.IsActive = StringToStatus(ProjectStatusString);
                projectToUpdate.Client = SelectedClient;
            }
            Shell.Current.GoToAsync("//ManageProjects");
        }

        public void Search()
        {
            RefreshView();
        }

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

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Clients));
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
