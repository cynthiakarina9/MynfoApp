﻿namespace Mynfo.ViewModels
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ProfilesByFacebookViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private List<ProfileSM> profilesM;
        #endregion

        #region Properties

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public List<ProfileSM> profileSM
        {
            get { return profilesM; }
            private set
            {
                SetValue(ref profilesM, value);
            }
        }
        #endregion

        #region Constructor
        public ProfilesByFacebookViewModel()
        {
            apiService = new ApiService();
            GetList();
        }
        
        private async Task<List<ProfileSM>> GetList()
        {
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            profileSM = new List<ProfileSM>();
            profileSM = await this.apiService.GetListByUser<ProfileSM>(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                MainViewModel.GetInstance().User.UserId);
            
            this.IsRunning = false;

            return profileSM;
        }
            #endregion

    }
}
