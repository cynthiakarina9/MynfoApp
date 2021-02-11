namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
    using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedListOfNetworksPage
    {
        public TabbedListOfNetworksPage(int _BoxId, bool _boxDefault, string _boxName)
        {
            InitializeComponent();
            //On<Android>().SetToolbarPlacement(Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfileTypeSelection = new ProfileTypeSelectionViewModel();
            mainViewModel.ListOfNetworks = new ListOfNetworksViewModel(_BoxId);

            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(50, 50));

            if (Device.RuntimePlatform == Device.iOS)
            {
                Children.Add(new ListOfNetworksPage(_BoxId) { IconImageSource = "ListProfiles_icon" });
                Children.Add(new ProfileTypeSelection(_BoxId, _boxDefault, _boxName) { IconImageSource = "TypesProfiles_Icon" });
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Children.Add(new ListOfNetworksPage(_BoxId) { IconImageSource = "lista.png" });
                Children.Add(new ProfileTypeSelection(_BoxId, _boxDefault, _boxName) { IconImageSource = "tipos.png" });
            }
            

            CurrentPage = Children[0];
        }
    }
}