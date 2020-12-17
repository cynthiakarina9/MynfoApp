namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Mynfo.Views;
    using Services;
    using System.Data.SqlClient;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class CreateProfileEmailViewModel : BaseViewModel
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
        public string Email
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public CreateProfileEmailViewModel()
        {
            this.apiService = new ApiService();

            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveProfileEmailCommand
        {
            get
            {
                return new RelayCommand(SaveProfileEmail);
            }
        }
        
        private async void SaveProfileEmail()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
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

            var profileEmail = new ProfileEmail
            {
                Name = this.Name,
                Email = this.Email,
                UserId = mainViewModel.User.UserId,
            };

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileemail = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                profileEmail);

            if (profileemail == default)
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
                mainViewModel.ProfilesBYPESM.addProfileEmail(profileemail);
                mainViewModel.ProfilesBYPESM = null;
            }
            else
            {
                mainViewModel.ProfilesByEmail.addProfile(profileemail);
            }

            this.Name = string.Empty;
            this.Email = string.Empty;

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
