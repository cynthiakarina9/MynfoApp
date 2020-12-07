using GalaSoft.MvvmLight.Command;
using Mynfo.Helpers;
using Mynfo.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mynfo.ViewModels
{

    public class PasswordRecoveryViewModel : BaseViewModel
    {

        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        bool isRunning;
        bool isEnabled;
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

        public string Email
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public PasswordRecoveryViewModel()
        {
            apiService = new ApiService();

            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand RecoveryCommand
        {
            get
            {
                return new RelayCommand(Recovery);
            }
        }

        async void Recovery()
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

            IsRunning = true;
            IsEnabled = false;

            var checkConnetion = await apiService.CheckConnection();
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

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            var response = await apiService.PasswordRecovery(
                apiSecurity,
                "/api",
                "/Users/PasswordRecovery",
                Email);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "We can't send the new password to this email.",
                    "OK");
                return;
            }

            IsRunning = false;
            IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                Languages.ConfirmLabel,
                "Your new password has been sent to your email!",
                Languages.Accept);

            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
