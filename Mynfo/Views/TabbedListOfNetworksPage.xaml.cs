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
            mainViewModel.ListOfNetworks = new ListOfNetworksViewModel(_BoxId, _boxDefault, _boxName);

            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(50, 50));

            Children.Add(new ProfileTypeSelection(_BoxId, _boxDefault, _boxName) { IconImageSource = "enter1.png" });
            Children.Add(new ListOfNetworksPage(_BoxId) { IconImageSource = "home_icon.png" });
            
            CurrentPage = Children[0];
        }
    }
}