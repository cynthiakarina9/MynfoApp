﻿namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Mynfo.Views;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class CreateProfileYoutubeViewModel : BaseViewModel
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
        public string Link
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public CreateProfileYoutubeViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand SaveProfileYoutubeCommand
        {
            get
            {
                return new RelayCommand(SaveProfileYouTube);
            }
        }

        private async void SaveProfileYouTube()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Link))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LinkValidation,
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

            var profileYoutube = new ProfileSM
            {
                ProfileName = this.Name,
                link = this.Link,
                UserId = mainViewModel.User.UserId,
                Exist = false,
                RedSocialId = 7
            };

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileSM = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                profileYoutube);

            if (profileSM == default)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorAddProfile,
                    Languages.Accept);
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            //Agregar a la lista
            if (mainViewModel.ProfilesBYPESM != null)
            {
                mainViewModel.ProfilesBYPESM.addProfileSM(profileSM);
            }
            else
            {
                mainViewModel.ProfilesByYoutube.addProfile(profileSM);
            }


            this.Name = string.Empty;
            this.Link = string.Empty;

            await App.Navigator.PopAsync();
        }
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
        #endregion
    }
}