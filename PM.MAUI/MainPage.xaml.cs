using PM.MAUI.ViewModels;

namespace PM.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
                
        }
    }
}