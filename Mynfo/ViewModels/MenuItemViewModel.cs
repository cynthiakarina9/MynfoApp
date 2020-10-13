namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Helpers;
    using Xamarin.Forms;
    using Views;
    using Mynfo.Models;

    public class MenuItemViewModel : BaseViewModel
    {
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
                //var mainViewModal = MainViewModel.GetInstance();
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
                MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                App.Navigator.PushAsync(new MyProfilePage());
            }

            else if (this.PageName == "ProfilesPage")
            {
                MainViewModel.GetInstance().Profiles = new ProfilesViewModel();
                App.Navigator.PushAsync(new ProfilesPage());
            }

            else if (this.PageName == "SettingsPage")
            {
                MainViewModel.GetInstance().Settings = new SettingsViewModel();
                App.Navigator.PushAsync(new SettingsPage());
            }
        }
        #endregion
    }
}
