namespace Mynfo.Views
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using System;
    using System.Threading;
    using System.Windows.Input;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Device = Xamarin.Forms.Device;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {        
        public SettingsPage()
        {
            InitializeComponent();
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #region Attributs
        private UserLocal user;
        #endregion
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }
        private void Navigate()
        {
            App.Master.IsPresented = false;
            var mainViewModal = MainViewModel.GetInstance();

            if (this.PageName == "LoginPage")
            {
                Settings.IsRemembered = "false";
                mainViewModal.Token = null;
                mainViewModal.User = null;
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.DeleteAll<UserLocal>();
                }
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.DeleteAll<TokenResponse>();
                }
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (this.PageName == "MyProfilePage")
            {
                //var user = MainViewModel.GetInstance().User;
                //if (user.UserTypeId == 1)
                //{
                MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                App.Navigator.PushAsync(new MyProfilePage());
                //}
                //else
                //{
                //    MainViewModel.GetInstance().MyExternalProfile = new MyExternalProfileViewModel();
                //    App.Navigator.PushAsync(new MyExternalProfilePage());
                //}

            }

            else if (this.PageName == "ProfilesPage")
            {
                MainViewModel.GetInstance().Profiles = new ProfilesViewModel();
                Application.Current.MainPage = new NavigationPage(new ProfilesPage());
            }

        }
        #endregion

        void escribir_tag(object sender, EventArgs e)
        {            
            var duration = TimeSpan.FromMilliseconds(500);
            Vibration.Vibrate(duration);
            Vibration.Vibrate(duration);
            //write_nfc = true;                                            
        }
    }
} 