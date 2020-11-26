namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
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

        #region Commands
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

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
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
            var response = await this.apiService.PutProfile(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps/PutProfileWhatsapp",
                profileWhats);

            this.IsRunning = false;
            this.IsEnabled = true;

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

            
            await App.Navigator.PopAsync();
        }
        #endregion

    }
}