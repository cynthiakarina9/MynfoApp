namespace Mynfo.ViewModels
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    public class ProfilesByEmailViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private List<ProfileEmail> profilemail;
        #endregion

        #region Properties
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
        public List<ProfileEmail> profileEmail 
        {
            get { return profilemail; } 
            private set 
            {
                SetValue(ref profilemail, value);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs(""));// Throw!!
                }
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
        public ProfilesByEmailViewModel()
        {
            apiService = new ApiService();
            GetList();
        }
        #endregion

        #region Commands
        public async Task<List<ProfileEmail>> GetList()
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

            profileEmail = new List<ProfileEmail>();
            profileEmail = await this.apiService.GetListByUser<ProfileEmail>(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                MainViewModel.GetInstance().User.UserId);
            return profileEmail;

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
