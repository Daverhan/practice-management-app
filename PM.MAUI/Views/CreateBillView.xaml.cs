using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

[QueryProperty(nameof(ProjectId), "projectId")]
public partial class CreateBillView : ContentPage
{
	public CreateBillView()
	{
		InitializeComponent();
	}

	public int ProjectId { get; set; }

	private void CreateBillClick(object sender, EventArgs e)
	{
		(BindingContext as ProjectViewModel).CreateBill();
		Shell.Current.GoToAsync("//ManageProjects");
	}

	private void CancelClick(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ManageProjects");
	}

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new ProjectViewModel(ProjectId);
    }
}