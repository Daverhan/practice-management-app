using PM.Library.Models;
using System.Windows.Input;

namespace PM.MAUI.ViewModels
{
    class BillViewModel
    {
        public Bill Model { get; set; }
        public ProjectViewModel SelectedProject { get; set; }

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ICommand DeleteCommand { get; set; }
        public void ExecuteDelete()
        {
            SelectedProject.Model.Bills.Remove(Model);
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command((b) => ExecuteDelete());
        }

        public BillViewModel(Bill bill, ProjectViewModel selectedProject)
        {
            Model = bill;
            SelectedProject = selectedProject;
            SetupCommands();
        }
    }
}
