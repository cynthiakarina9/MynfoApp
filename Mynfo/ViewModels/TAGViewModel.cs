namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Services;
    using Mynfo.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class TAGViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Constructor
        public TAGViewModel()
        {
            apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand BackHomeCommand
        {
            get
            {
                return new RelayCommand(BackHome);
            }
        }
        private void BackHome()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }

        public ICommand GoToStoreCommand
        {
            get
            {
                return new RelayCommand(GoToStore);
            }
        }
        private async void GoToStore()
        {
            await Launcher.OpenAsync(new Uri("https://mynfo.mx/index.php/tienda/"));
        }
        #endregion
    }
}
