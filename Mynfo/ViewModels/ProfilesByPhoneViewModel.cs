﻿namespace Mynfo.ViewModels
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ProfilesByPhoneViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private List<ProfilePhone> profilePhone;
        #endregion

        #region Properties

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public List<ProfilePhone> Profilephone
        {
            get { return profilePhone; }
            private set
            {
                SetValue(ref profilePhone, value);
            }
        }
        #endregion
        #region Constructor
        public ProfilesByPhoneViewModel()
        {
            apiService = new ApiService();
            GetList();
        }
        private async Task<List<ProfilePhone>> GetList()
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

            Profilephone = new List<ProfilePhone>();
            Profilephone = await this.apiService.GetListByUser<ProfilePhone>(
                apiSecurity,
                "/api",
                "/ProfilePhones",
                MainViewModel.GetInstance().User.UserId);

            return Profilephone;
        }
        #endregion
    }
}
