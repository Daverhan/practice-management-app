using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

public partial class ManageClientsView : ContentPage
{
	public ManageClientsView()
	{
		InitializeComponent();
		BindingContext = new ManageClientsViewModel();
	}

    private void SearchClick(object sender, EventArgs e)
    {
        (BindingContext as ManageClientsViewModel).Search();
    }

    private void AddClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ClientDetail");
    }

    private void EditClick(object sender, EventArgs e)
    {
        (BindingContext as ManageClientsViewModel).EditClientClick(Shell.Current);
    }

    private void DeleteClick(object sender, EventArgs e)
    {
        (BindingContext as ManageClientsViewModel).Delete();
    }

    private void ExitClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ManageClientsViewModel).RefreshView();
    }
}