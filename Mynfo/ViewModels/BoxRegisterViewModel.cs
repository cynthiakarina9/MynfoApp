namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Mynfo.Models;
    using Services;
    using System;
    using System.Data.SqlClient;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class BoxRegisterViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public string Name
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public BoxRegisterViewModel()
        {
            this.apiService = new ApiService();

            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveBoxCommand
        {
            get
            {
                return new RelayCommand(SaveBox);
            }
        }
        private async void SaveBox()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            DateTime boxTime = DateTime.Now;
            bool defaultBoxExists = false;
            Box box;
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var BoxList = await this.apiService.GetBoxDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxDefault",
                MainViewModel.GetInstance().User.UserId);
            if(BoxList == default || BoxList == null)
            {
                defaultBoxExists = false;
            }
            else
            {
                defaultBoxExists = true;
            }

            if (defaultBoxExists)
            {
                box = new Box
                {
                    Name = this.Name,
                    BoxDefault = false,
                    UserId = mainViewModel.User.UserId,
                    Time = boxTime,
                    ColorBox = "#c6c6c6"
                };
            }
            else
            {
                box = new Box
                {
                    Name = this.Name,
                    BoxDefault = true,
                    UserId = mainViewModel.User.UserId,
                    Time = boxTime,
                    ColorBox = "#c6c6c6"
                };
            }


            var response = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Boxes",
                box);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }
            var A = await this.apiService.GetLastBox(
                apiSecurity,
                "/api",
                "/Boxes/GetLastBox",
                mainViewModel.User.UserId);
            this.IsRunning = false;
            this.IsEnabled = true;

            this.Name = string.Empty;
            mainViewModel.Home.AddList(A);
            await App.Navigator.PopAsync();
        }

        public ICommand BackHomeCommand
        {
            get
            {
                return new RelayCommand(BackHome);
            }
        }
        private  void BackHome()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}
