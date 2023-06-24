using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

public partial class ManageEmployeesView : ContentPage
{
	public ManageEmployeesView()
	{
		InitializeComponent();
		BindingContext = new ManageEmployeesViewModel();
	}

	private void AddClick(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//EmployeeDetail");
	}

	private void EditClick(object sender, EventArgs e)
	{
		(BindingContext as ManageEmployeesViewModel).RefreshView();
	}

	private void SearchClick(object sender, EventArgs e)
	{
        (BindingContext as ManageEmployeesViewModel).RefreshView();
    }

	private void DeleteClick(object sender, EventArgs e)
	{
        (BindingContext as ManageEmployeesViewModel).RefreshView();
    }

	private void ExitClick(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as ManageEmployeesViewModel).RefreshView();
	}
}