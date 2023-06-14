using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

[QueryProperty(nameof(ProjectId), "projectId")]
public partial class ProjectDetailView : ContentPage
{
	public ProjectDetailView()
	{
		InitializeComponent();
	}

    public int ProjectId { get; set; }

    private void SearchClick(object sender, EventArgs e)
    {
        (BindingContext as ProjectDetailViewModel).Search();
    }

    private void ConfirmClick(object sender, EventArgs e)
    {
        (BindingContext as ProjectDetailViewModel).AddProject();
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ManageProjects");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectDetailViewModel(ProjectId);
    }
}