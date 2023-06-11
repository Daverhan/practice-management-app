using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

[QueryProperty(nameof(EmployeeId), "employeeId")]
public partial class EmployeeDetailView : ContentPage
{
	public EmployeeDetailView()
	{
		InitializeComponent();
	}

	public int EmployeeId { get; set; }

	private void ConfirmClick(object sender, EventArgs e)
	{
		(BindingContext as EmployeeDetailViewModel).AddEmployee();
	}

	private void CancelClick(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ManageEmployees");
	}

	private void OnLeaving(object sender, NavigatedFromEventArgs e)
	{
		BindingContext = null;
	}

	private void OnArriving(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new EmployeeDetailViewModel(EmployeeId);
	}
}