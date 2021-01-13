namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class Register2ViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private User user1;
        private bool isCheck;
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
        public bool IsCheck
        {
            get { return this.isCheck; }
            set { SetValue(ref this.isCheck, value); }
        }
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Confirm
        {
            get;
            set;
        }

        public string Number
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public Register2ViewModel(User user)
        {
            user1 = new User();
            user1 = user;
            this.apiService = new ApiService();

            this.IsEnabled = true;
            this.IsCheck = false;
        }
        #endregion

        #region Methods

        #endregion

        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }
            if (!RegexUtilities.IsValidEmail(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation2,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                return;
            }

            if (this.Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation2,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConfirmValidation,
                    Languages.Accept);
                return;
            }
            if (this.Password != this.Confirm)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConfirmValidation2,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Number))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NumberValidation,
                    Languages.Accept);
                return;
            }
            if (this.Number.Length != 10)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneValidation2,
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

            var user2 = new User
            {
                Email = this.Email,
                FirstName = user1.FirstName,
                LastName = user1.LastName,
                ImageArray = user1.ImageArray,
                UserTypeId = user1.UserTypeId,
                Password = this.Password
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            var exists = await this.apiService.GetUserByEmail(
                apiSecurity,
                "/api",
                "/Users/GetUserByEmail",
                this.Email);

            if (exists != null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.WrongEmail,
                    Languages.Accept);
                return;
            }

            var response = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Users",
                user2);

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
            var exists2 = await this.apiService.GetUserByEmail(
                apiSecurity,
                "/api",
                "/Users/GetUserByEmail",
                this.Email);

            if (IsCheck == false)
            {
                var ProfileP = new ProfilePhone
                {
                    Exist = false,
                    Name = user2.FirstName,
                    Number = Number,
                    UserId = exists2.UserId
                };
                var profilephone = await this.apiService.Post(
                    apiSecurity,
                    "/api",
                    "/ProfilePhones",
                    ProfileP);

                if (profilephone == default)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.ErrorAddProfile,
                        Languages.Accept);
                    return;
                }
            }
            else
            {
                var profileWhatsApp = new ProfileWhatsapp
                {
                    Name = user2.FirstName,
                    Number = this.Number,
                    UserId = exists2.UserId,
                    Exist = false
                };

                var profileWhatsapp = await this.apiService.Post(
                    apiSecurity,
                    "/api",
                    "/ProfileWhatsapps",
                    profileWhatsApp);

                if (profileWhatsapp == default)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.ErrorAddProfile,
                        Languages.Accept);
                    return;
                }
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                Languages.ConfirmLabel,
                Languages.UserRegisteredMessage,
                Languages.Accept);
            MainViewModel.GetInstance().Login = new LoginViewModel();
            Application.Current.MainPage = new LoginPage();
        }
            #endregion
    }
}
