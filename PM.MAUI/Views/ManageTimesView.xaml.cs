using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

public partial class ManageTimesView : ContentPage
{
	public ManageTimesView()
	{
		InitializeComponent();
        BindingContext = new ManageTimesViewModel();
	}

    private void AddClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//TimeDetail");
    }

    private void EditClick(object sender, EventArgs e)
    {
        (BindingContext as ManageTimesViewModel).RefreshView();
    }

    private void SearchClick(object sender, EventArgs e)
    {
        (BindingContext as ManageTimesViewModel).RefreshView();
    }

    private void DeleteClick(object sender, EventArgs e)
    {
        (BindingContext as ManageTimesViewModel).RefreshView();
    }

    private void ExitClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ManageTimesViewModel).RefreshView();
    }
}