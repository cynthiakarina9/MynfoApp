namespace Mynfo.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageIOS : FlyoutPage
    {
        MenuPage menuPage;
        public MasterPageIOS()
        {
            menuPage = new MenuPage();
            Flyout = menuPage;
            Detail = new NavigationPage(new TabbedPage1());
        }
    }
}
