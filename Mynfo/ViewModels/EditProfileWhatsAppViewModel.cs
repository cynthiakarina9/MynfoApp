namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class EditProfileWhatsAppViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ProfileWhatsapp profilewhats;
        #endregion

        #region Properties
        public ProfileWhatsapp profileWhats
        {
            get { return profilewhats; }
            private set
            {
                SetValue(ref profilewhats, value);
            }
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
        public EditProfileWhatsAppViewModel( int _ProfileMSId)
        {
            this.apiService = new ApiService();
            GetProfile(_ProfileMSId);
            this.isEnabled = true;
        }
        #endregion

        #region Methods
        private async Task<ProfileWhatsapp> GetProfile(int _ProfileMSId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            profileWhats = new ProfileWhatsapp();
            profileWhats = await this.apiService.GetProfileWhatsApp(
               apiSecurity,
               "/api",
               "/ProfileWhatsapps/GetProfileWhatsApp",
               _ProfileMSId);
            return profileWhats;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.profileWhats.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.profileWhats.Number))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NumberValidation,
                    Languages.Accept);
                return;
            }
            if (!(this.profileWhats.Number).ToCharArray().All(Char.IsDigit))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NumberValidation,
                    Languages.Accept);
                return;
            }
            if (this.profileWhats.Number.Length != 10)
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
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profile = await this.apiService.PutProfile(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps/PutProfileWhatsapp",
                profileWhats);

            this.IsRunning = false;
            this.IsEnabled = true;


            //Agregar a la lista
            MainViewModel.GetInstance().ProfilesByWhatsApp.updateProfile(profile);

            await App.Navigator.PopAsync();
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }
        private async void Delete()
        {
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
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.DeleteWhatsapp(
                apiSecurity,
                "/api",
                "/Box_ProfileWhatsapp/DeleteBox_ProfileWhatsappRelations",
                profileWhats.ProfileWhatsappId);

            var response2 = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                profileWhats.ProfileWhatsappId);

            this.IsRunning = false;
            this.IsEnabled = true;

            MainViewModel.GetInstance().ProfilesByWhatsApp.removeProfile();

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