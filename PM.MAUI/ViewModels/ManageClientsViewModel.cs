﻿using PM.Library.DTO;
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
        public ProjectDTO SelectedProject { get; set; }
        public string Query { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string DateOpened { get; set; }
        public string DateClosed { get; set; }
        public string Notes { get; set; }
        public string ProjectsMessage { get; set; }
        public string BillsMessage { get; set; }
        public ObservableCollection<ProjectDTO> AssociatedProjects { get; set; }
        public ObservableCollection<Bill> AssociatedBills { get; set; }

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

        public void UpdateSelectedClientDetails()
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
            AssociatedProjects = new ObservableCollection<ProjectDTO>(ProjectService.Current.Projects.Where(p => p.Client.Id == SelectedClient.Model.Id).ToList());

            BillsMessage = null;
            AssociatedBills = null;
            SelectedProject = null;

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Status));
            NotifyPropertyChanged(nameof(DateOpened));
            NotifyPropertyChanged(nameof(DateClosed));
            NotifyPropertyChanged(nameof(Notes));
            NotifyPropertyChanged(nameof(ProjectsMessage));
            NotifyPropertyChanged(nameof(AssociatedProjects));
            NotifyPropertyChanged(nameof(BillsMessage));
            NotifyPropertyChanged(nameof(AssociatedBills));
            NotifyPropertyChanged(nameof(SelectedProject));
        }

        public void UpdateSelectedProjectDetails()
        {
            if(SelectedProject == null)
            {
                return;
            }

            BillsMessage = "Bills:";
            AssociatedBills = new ObservableCollection<Bill>(ProjectService.Current.GetProject(SelectedProject.Id).Bills);

            NotifyPropertyChanged(nameof(BillsMessage));
            NotifyPropertyChanged(nameof(AssociatedBills));
        }

        public void RefreshView()
        {
            Name = Status = DateOpened = DateClosed = Notes = ProjectsMessage = BillsMessage = null;
            AssociatedProjects = null;
            AssociatedBills = null;
            SelectedProject = null;
            SelectedClient = null;


            NotifyPropertyChanged(nameof(Clients));

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Status));
            NotifyPropertyChanged(nameof(DateOpened));
            NotifyPropertyChanged(nameof(DateClosed));
            NotifyPropertyChanged(nameof(Notes));
            NotifyPropertyChanged(nameof(ProjectsMessage));
            NotifyPropertyChanged(nameof(SelectedClient));
            NotifyPropertyChanged(nameof(SelectedProject));
            NotifyPropertyChanged(nameof(BillsMessage));
            NotifyPropertyChanged(nameof(AssociatedBills));
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
