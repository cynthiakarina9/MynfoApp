namespace Mynfo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Services;
    using Xamarin.Forms;

    public class EditProfileEmailViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private ProfileEmail profilemail;
        #endregion

        #region Properties
        public ProfileEmail profileEmail
        {
            get { return profilemail; }
            private set
            {
                SetValue(ref profilemail, value);
            }
        }

        #endregion

        #region Constructor
        public EditProfileEmailViewModel(int _ProfileEmailId)
        {
            apiService = new ApiService();
            GetProfileEmail( _ProfileEmailId);
        }
        #endregion

        #region Commands
        private async Task<ProfileEmail> GetProfileEmail(int _ProfileEmailId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            profileEmail = new ProfileEmail();
            profileEmail = await this.apiService.GetProfileEmail(
               apiSecurity,
               "/api",
               "/ProfileEmails/GetProfileEmail",
               _ProfileEmailId);
            return profileEmail;
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
                "/Users",
                profileEmail.ProfileEmailId);
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
