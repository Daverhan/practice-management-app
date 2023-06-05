using PM.MAUI.ViewModels;

namespace PM.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void ManageClientsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ManageClients");
        }

        private void ManageProjectsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ManageProjects");
        }
    }
}