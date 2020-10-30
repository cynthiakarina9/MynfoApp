namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditProfileEmailViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public ProfileEmail profileEmail
        {
            get;
            set;
        }
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
        #endregion

        #region Constructor
        public EditProfileEmailViewModel( int _ProfileEmailId = 0)
        {
            //var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            //ProfileEmail EmailProfile = new ProfileEmail();
            //EmailProfile.ProfileEmailId = _ProfileEmailId;
            //var EmailProfile2 = this.apiService.Get(
            //    apiSecurity,
            //    "/api",
            //    "/ProfileEmails",
            //    MainViewModel.GetInstance().Token.TokenType,
            //    MainViewModel.GetInstance().Token.AccessToken,
            //    EmailProfile.ProfileEmailId);// profileEmail.ProfileEmailId);
            //ProfileEmail EmailLocal = EmailProfile;
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
        #endregion

        #region Commands

        public ICommand SaveProfileCommand
        {
            get
            {
                return new RelayCommand(SaveProfileChange);
            }
        }

        private async void SaveProfileChange()
        {
            //if (string.IsNullOrEmpty(this.profileEmail.Name))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.LastNameValidation,
            //        Languages.Accept);
            //    return;
            //}

            //if (string.IsNullOrEmpty(this.profileEmail.Email))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.EmailValidation,
            //        Languages.Accept);
            //    return;
            //}

            //if (!RegexUtilities.IsValidEmail(this.profileEmail.Email))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.EmailValidation2,
            //        Languages.Accept);
            //    return;
            //}

            //this.IsRunning = true;
            //this.IsEnabled = false;

            //var checkConnetion = await this.apiService.CheckConnection();
            //if (!checkConnetion.IsSuccess)
            //{
            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        checkConnetion.Message,
            //        Languages.Accept);
            //    return;
            //}
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            //var response2 = await this.apiService.Get<ProfileEmail>(
            //    apiSecurity,
            //    "/api",
            //    "/ProfileEmails",
            //    MainViewModel.GetInstance().Token.TokenType,
            //    MainViewModel.GetInstance().Token.AccessToken,
            //    profileEmail.ProfileEmailId);
            

            var response = await this.apiService.Put(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                MainViewModel.GetInstance().Token.TokenType,
                MainViewModel.GetInstance().Token.AccessToken,
                profileEmail);

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

            await App.Navigator.PopAsync();
        }
        #endregion

    }
}