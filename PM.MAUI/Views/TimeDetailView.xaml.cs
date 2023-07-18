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
		(BindingContext as TimeViewModel).RefreshProjectsList();
	}

	private void SearchEmployeeClick(object sender, EventArgs e)
	{
		(BindingContext as TimeViewModel).RefreshEmployeesList();
	}

	private void ConfirmClick(object sender, EventArgs e)
	{
		if((BindingContext as TimeViewModel).AddOrUpdate())
		{
            Shell.Current.GoToAsync("//ManageTimes");
        }
	}

	private void CancelClick(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ManageTimes");
	}

	private void OnArriving(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new TimeViewModel(TimeId);
	}
}