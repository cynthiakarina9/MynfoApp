namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Services;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class EditProfileFacebookViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private ProfileSM profileSm;
        #endregion

        #region Properties
        public ProfileSM profileSM
        {
            get { return profileSm; }
            private set
            {
                SetValue(ref profileSm, value);
            }
        }
        #endregion
        #region Constructor
        public EditProfileFacebookViewModel(int _ProfileMSId)
        {
            apiService = new ApiService();
            GetProfile(_ProfileMSId);
        }
        #endregion

        #region Commands
        private async Task<ProfileSM> GetProfile(int _ProfileMSId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            profileSM = new ProfileSM();
            profileSM = await this.apiService.GetProfileSM(
               apiSecurity,
               "/api",
               "/ProfileSMs/GetProfileSM",
               _ProfileMSId);
            return profileSM;
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
            //ButtonSave.IsEnabled = false;
            //ButtonDelete.IsEnabled = false;
            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                //this.IsRunning = false;
                //ButtonSave.IsEnabled = true;
                //ButtonDelete.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Put(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                profileSM.ProfileMSId);
            await App.Navigator.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
