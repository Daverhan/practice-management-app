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
        (BindingContext as ProjectViewModel).RefreshView();
    }

    private void ConfirmClick(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).AddOrUpdate();
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