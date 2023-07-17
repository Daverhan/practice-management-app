using PM.Library.DTO;
using PM.Library.Models;
using PM.Library.Services;
using PM.Library.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PM.MAUI.ViewModels
{
    class ProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ClientDTO SelectedClient { get; set; }
        public DateTime SelectedDate { get; set; }
        public string Query { get; set; }
        public ProjectDTO Model { get; set; }
        public string ProjectStatusString { get; set; }

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ICommand CreateBillCommand { get; private set; }
        public void ExecuteCreateBill(int id)
        {
            Shell.Current.GoToAsync($"//CreateBill?projectId={id}");
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
            CreateBillCommand = new Command((p) => ExecuteCreateBill((p as ProjectViewModel).Model.Id));
        }

        public ProjectViewModel(ProjectDTO project)
        {
            Model = project;
            SetupCommands();
        }

        public ProjectViewModel(int projectId)
        {
            if(projectId == 0)
            {
                Model = new ProjectDTO { IsActive = true, Bills = new List<Bill>()};
            }
            else
            {
                Model = ProjectService.Current.GetProject(projectId);
                SelectedClient = Model.Client;
            }

            SelectedDate = DateTime.Now;
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

        public void CreateBill()
        {
            decimal totalAmount = 0;

            foreach(var time in TimeService.Current.Times) {
                if(time.Project.Equals(Model))
                {
                    totalAmount += time.Employee.Rate * time.Hours;
                }
            }

            Model.Bills.Add(new Bill { DueDate = SelectedDate, TotalAmount = totalAmount});
            var response = new WebRequestHandler().Post("/Project", Model).Result;
        }

        public ObservableCollection<ClientDTO> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<ClientDTO>(ClientService.Current.Clients);
                }
                return new ObservableCollection<ClientDTO>(ClientService.Current.Search(Query));
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
