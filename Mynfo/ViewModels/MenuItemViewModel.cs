namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Helpers;
    using Xamarin.Forms;
    using Views;

    public class MenuItemViewModel
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
            if (this.PageName == "LoginPage")
            {
                Settings.Token = string.Empty;
                Settings.TokenType = string.Empty;
                var mainViewModal = MainViewModel.GetInstance();
                mainViewModal.Token = string.Empty;
                mainViewModal.TokenType = string.Empty;
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (this.PageName == "MyProfilePage")
            {
                //MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                //App.Navigator.PushAsync(new MyProfilePage());
            }
            
        }
        #endregion
    }
}
