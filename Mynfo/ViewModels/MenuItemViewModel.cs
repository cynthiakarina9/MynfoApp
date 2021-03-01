namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Helpers;
    using Xamarin.Forms;
    using Views;
    using Mynfo.Models;
    using Mynfo.Services;
    using Xamarin.Essentials;
    using Rg.Plugins.Popup.Services;

    public class MenuItemViewModel : BaseViewModel
    {
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        public bool Share { get; set; }
        public static bool nfc_status { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }
        private async void Navigate()
        {
            App.Master.IsPresented = false;
            var mainViewModal = MainViewModel.GetInstance();
            //Logout
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
                //Borrar la box local
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.DeleteAll<BoxLocal>();
                }
                //Borrar perfiles locales
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.DeleteAll<ProfileLocal>();
                }
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.DeleteAll<ForeingBox>();
                }
                //Borrar perfiles locales
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.DeleteAll<ForeingProfile>();
                }
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (this.PageName == "MyProfilePage")
            {
                MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                await App.Navigator.PushAsync(new MyProfilePage());
            }

            else if (this.PageName == "QR")
            {
                await App.Navigator.PushAsync(new QRTabbedPage());
            }
 
            else if ((this.PageName == "TAGPage") && (nfc_status == true))
            {
                MainViewModel.GetInstance().TAG = new TAGViewModel();
                await App.Navigator.PushAsync(new TAGPage());
            }

            else if (this.PageName == "Help")
            {
                MainViewModel.GetInstance().GIF = new GifViewModel("All");
                await PopupNavigation.Instance.PushAsync(new GifPage());
            }

            else if (this.PageName == "Comments")
            {
                await Browser.OpenAsync("https://mynfo.mx/preguntas-frecuentes/", BrowserLaunchMode.SystemPreferred);
            }
        }
        #endregion
    }
}