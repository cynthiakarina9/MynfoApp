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
            //mainViewModel.MyProfile = new MyProfileViewModel();
            //mainViewModel.ChangePassword = new ChangePasswordViewModel();
            mainViewModel.Profiles = new ProfilesViewModel();
            //mainViewModel.Settings = new SettingsViewModel();
            mainViewModel.ListForeignBox = new ListForeignBoxViewModel();

            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(100, 100));

            Children.Add(new ProfilesPage {IconImageSource = "networks_icon.png" });
            Children.Add(new HomePage {IconImageSource = "home_icon.png"});
            Children.Add(new ListForeignBoxPage {IconImageSource = "connections_icon.png"});
            CurrentPage = Children[1];
        }
    }
}