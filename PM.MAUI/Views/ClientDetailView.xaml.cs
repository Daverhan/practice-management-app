using PM.Library.Services;
using PM.MAUI.ViewModels;

namespace PM.MAUI.Views;

[QueryProperty(nameof(ClientId), "clientId")]
public partial class ClientDetailView : ContentPage
{
    public int ClientId { get; set; }

	public ClientDetailView()
	{
		InitializeComponent();
	}

    private void ConfirmClick(object sender, EventArgs e)
    {
        if((BindingContext as ClientViewModel).AddOrUpdate())
        {
            Shell.Current.GoToAsync("//ManageClients");
        }
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ManageClients");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ClientViewModel(ClientId);
    }
}