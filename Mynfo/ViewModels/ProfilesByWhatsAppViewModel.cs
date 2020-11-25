namespace Mynfo.ViewModels
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ProfilesByWhatsAppViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private List<ProfileWhatsapp> profileWhatsapp;
        #endregion

        #region Properties
        public List<ProfileWhatsapp> profileWhatsApp
        {
            get { return profileWhatsapp; }
            private set
            {
                SetValue(ref profileWhatsapp, value);

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

        #region Constructors
        public ProfilesByWhatsAppViewModel()
        {
            apiService = new ApiService();
            GetList();
        }
        #endregion

        #region Commands
        public async Task<List<ProfileWhatsapp>> GetList()
        {
            this.IsRunning = true;
            this.isEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            profileWhatsApp = new List<ProfileWhatsapp>();
            profileWhatsApp = await this.apiService.GetListByUser<ProfileWhatsapp>(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                MainViewModel.GetInstance().User.UserId);
            return profileWhatsApp;

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
