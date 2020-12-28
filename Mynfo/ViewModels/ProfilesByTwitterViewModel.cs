using Mynfo.Domain;
using Mynfo.Helpers;
using Mynfo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mynfo.ViewModels
{
    public class ProfilesByTwitterViewModel: BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private ObservableCollection<ProfileSM> profilesM;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public ObservableCollection<ProfileSM> profileSM
        {
            get { return profilesM; }
            private set
            {
                SetValue(ref profilesM, value);
            }
        }

        public ProfileSM selectedProfile { get; set; }
        #endregion

        #region Constructor
        public ProfilesByTwitterViewModel()
        {
            apiService = new ApiService();
            GetList();
        }
        #endregion

        #region Methods
        private async Task<ObservableCollection<ProfileSM>> GetList()
        {
            this.IsRunning = true;

            List<ProfileSM> profileSocialMedia;
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

            profileSM = new ObservableCollection<ProfileSM>();

            profileSocialMedia = await this.apiService.GetListByUser<ProfileSM>(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                MainViewModel.GetInstance().User.UserId);

            this.IsRunning = false;
            return new ObservableCollection<ProfileSM>();
        }
        #endregion
    }
}
