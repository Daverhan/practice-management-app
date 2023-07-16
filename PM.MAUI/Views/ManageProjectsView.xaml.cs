using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

public partial class ManageProjectsView : ContentPage
{
	public ManageProjectsView()
	{
		InitializeComponent();
		BindingContext = new ManageProjectsViewModel();
	}

    private void AddClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProjectDetail");
    }

    private void EditClick(object sender, EventArgs e)
    {
        (BindingContext as ManageProjectsViewModel).RefreshView();
    }

    private void SearchClick(object sender, EventArgs e)
    {
        (BindingContext as ManageProjectsViewModel).RefreshView();
    }

    private void DeleteClick(object sender, EventArgs e)
    {
        (BindingContext as ManageProjectsViewModel).RefreshView();
    }

    private void BillDeletionClick(object sender, EventArgs e)
    {
        (BindingContext as ManageProjectsViewModel).RefreshBills();
    }

    private void ExitClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ManageProjectsViewModel).RefreshView();
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        (BindingContext as ManageProjectsViewModel).UpdateSelectedDetails();
    }
}