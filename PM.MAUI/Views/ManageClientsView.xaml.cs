using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

public partial class ManageClientsView : ContentPage
{
	public ManageClientsView()
	{
		InitializeComponent();
		BindingContext = new ManageClientsViewModel();
	}

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ManageClientsViewModel).Search();
    }

    private void DeleteClick(object sender, EventArgs e)
    {
        (BindingContext as ManageClientsViewModel).Delete();
    }

    private void ExitClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}