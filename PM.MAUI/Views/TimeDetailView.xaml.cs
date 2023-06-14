using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

[QueryProperty(nameof(TimeId), "timeId")]
public partial class TimeDetailView : ContentPage
{
	public TimeDetailView()
	{
		InitializeComponent();
	}

	public int TimeId { get; set; }

	private void SearchProjectClick(object sender, EventArgs e)
	{
		(BindingContext as TimeDetailViewModel).SearchProject();
	}

	private void SearchEmployeeClick(object sender, EventArgs e)
	{
		(BindingContext as TimeDetailViewModel).SearchEmployee();
	}

	private void ConfirmClick(object sender, EventArgs e)
	{
		(BindingContext as TimeDetailViewModel).AddTime();
	}

	private void CancelClick(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ManageTimes");
	}

	private void OnLeaving(object sender, NavigatedFromEventArgs e)
	{
		BindingContext = null;
	}

	private void OnArriving(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new TimeDetailViewModel(TimeId);
	}
}