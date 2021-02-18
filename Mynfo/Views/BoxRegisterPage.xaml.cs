namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using Xamarin.Forms;
    public partial class BoxRegisterPage : ContentPage
    {
        public BoxRegisterPage()
        {
            InitializeComponent();
            OSAppTheme currentTheme = App.Current.RequestedTheme;
            if (currentTheme == OSAppTheme.Dark)
            {
                Logosuperior.Source = "logo_superior2.png";
            }
            else
            {
                Logosuperior.Source = "logo_superior3.png";
            }
        }
    }
}