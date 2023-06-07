using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

[QueryProperty(nameof(ClientId), "clientId")]
public partial class ClientDetailView : ContentPage
{
	public ClientDetailView()
	{
		InitializeComponent();
	}

	public int ClientId { get; set; }

    private void ConfirmClick(object sender, EventArgs e)
    {
		(BindingContext as ClientDetailViewModel).AddClient();
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ManageClients");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
	{
		BindingContext = null;
    }

	private void OnArriving(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new ClientDetailViewModel(ClientId);
	}

}