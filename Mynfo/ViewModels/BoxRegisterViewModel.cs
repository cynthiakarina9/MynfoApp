namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Domain;
    using Services;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;
    using System;

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
                        Languages.FirstNameValidation,
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
            var box = new Box
                {
                    Name = this.Name,
                    BoxDefault = false,
                    UserId = mainViewModel.User.UserId,
                    Time = boxTime,
                };

                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Post(
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

                this.IsRunning = false;
                this.IsEnabled = true;

            mainViewModel.Home = new HomeViewModel();
            App.Current.MainPage = new DetailsProfilePage();


        }
        #endregion

       
    }
}
