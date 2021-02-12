namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
    using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1
    {
        public TabbedPage1()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Profiles = new ProfilesViewModel();
            mainViewModel.ListForeignBox = new ListForeignBoxViewModel();

            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(50, 50));
            BackgroundColor = Color.Transparent;
            if (Device.RuntimePlatform == Device.iOS)
            {
                Children.Add(new ProfilesPage { IconImageSource = "Networks_icon" });
                Children.Add(new HomePage { IconImageSource = "Home_icon" });
                Children.Add(new ListForeignBoxPage { IconImageSource = "Connections_icon" });
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Children.Add(new ProfilesPage {IconImageSource = "networks_icon.png"});
                Children.Add(new HomePage { IconImageSource = "home_icon.png" });
                Children.Add(new ListForeignBoxPage { IconImageSource = "connections_icon.png" });
            }
            
            CurrentPage = Children[1];
        }
    }
}