namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Services;
    using Mynfo.Views;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProfileTypeSelectionViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        public Box box;
        #endregion

        #region Properties
        public Box Box
        {
            get { return box; }
            private set
            {
                SetValue(ref box, value);
            }
        }
        #endregion

        #region Constructor
        public ProfileTypeSelectionViewModel(int _BoxId)
        {
            apiService = new ApiService();
            GetBox();
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

        public ICommand GotoAddCommand
        {
            get
            {
                return new RelayCommand(GotoAdd);
            }
        }
        private void GotoAdd()
        {
            PopupNavigation.Instance.PopAsync();
            MainViewModel.GetInstance().ListOfNetworks = new ListOfNetworksViewModel(Box.BoxId);
            PopupNavigation.Instance.PushAsync(new ListOfNetworksPage(Box.BoxId));
        }

        public ICommand GoBackCommand
        {
            get
            {
                return new RelayCommand(GoBack);
            }
        }
        private void GoBack()
        {
            PopupNavigation.Instance.PopAsync();
        }
        #endregion

        #region Methods

        #region Box
        public async Task<Box> GetBox()
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            Box = new Box();
            Box = await this.apiService.GetBox(
                apiSecurity,
                "/api",
                "/Boxes",
                MainViewModel.GetInstance().User.UserId);
            return Box;
        }
        #endregion

        #endregion
    }
}
