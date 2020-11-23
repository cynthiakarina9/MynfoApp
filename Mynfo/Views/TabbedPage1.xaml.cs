namespace Mynfo.Views
{
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using System.Collections.ObjectModel;
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
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.MyProfile = new MyProfileViewModel();
            mainViewModel.ChangePassword = new ChangePasswordViewModel();
            mainViewModel.Profiles = new ProfilesViewModel();
            mainViewModel.Settings = new SettingsViewModel();
            mainViewModel.ListForeignBox = new ListForeignBoxViewModel();

            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(24, 24));

            Children.Add(new HomePage { Title = Languages.Home, IconImageSource = "home1.png" });
            Children.Add(new ProfilesPage { Title = Languages.MyProfiles, IconImageSource = "perfiles.png" });
            Children.Add(new ListForeignBoxPage { Title = Languages.ReceivedBoxes, IconImageSource = "Conections.png" });
        }
    }
}